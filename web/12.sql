DROP TABLE IF EXISTS orders_import_lines;
CREATE TABLE orders_import_lines (
  id serial PRIMARY KEY,
  source_file text NOT NULL,   -- имя файла/источника
  line_no int NOT NULL,        -- номер строки
  raw_line text NOT NULL,      -- необработанная строка
  imported_at timestamptz default now(),
  note text
);

INSERT INTO orders_import_lines (source_file, line_no, raw_line, note) VALUES
-- Контакты покупателей: email в угловых скобках и простые варианты, телефоны в разных форматах
('marketplace_A_2025_11.csv', 1, 'Order#1001; Customer: Olga Petrova <olga.petrova@example.com>; +7 (921) 555-12-34; Items: SKU:AB-123-XY x1', 'order row'),
('marketplace_A_2025_11.csv', 2, 'Order#1002; Customer: Ivan <ivan@@example..com>; 8-921-5551234; Items: SKU:zx9999 x2', 'order row'),
('newsletter_upload.csv', 10, 'john.doe@domain.com; +44 7700 900123; tags: promo, holiday', 'marketing upload'),

-- Цены с разделителями тысяч и валютой
('pricing_feed.csv', 3, 'product: ZX-11; price: "1,299.99" USD', 'price row'),
('pricing_feed.csv', 4, 'product: Y-200; price: "2 500,00" EUR', 'price row'),

-- Теги/категории в поле tags:
('catalog_tags.csv', 1, 'tags: electronics, mobile,  accessories', 'tags row'),
('catalog_tags.csv', 2, 'tags: home,kitchen', 'tags row'),

-- «Грязные» CSV-строки: запятые внутри полей, кавычки
('orders_dirty.csv', 5, '"Smith, John","12 Baker St, Apt 4","1,200.00","SKU: AB-123-XY"', 'dirty csv'),

-- Логи обработки: разного регистра, ошибки и предупреждения
('processor_log.txt', 100, 'INFO: Processing order 1001', 'log'),
('processor_log.txt', 101, 'warning: price parse failed for line 4', 'log'),
('processor_log.txt', 102, 'Error: invalid phone for order 1002', 'log'),
('processor_log.txt', 103, 'error: missing sku in items list', 'log'),

-- Ловушки / edge-cases для проверки наивных regex
('marketplace_A_2025_11.csv', 20, 'Customer: bad@-domain.com; +7 921 ABC-12-34; Items: SKU: 12-AB-!!', 'trap-invalid-email-phone-sku'),
('orders_dirty.csv', 6, '"O''Connor, Liam","New York, NY","500"', 'dirty csv with apostrophe');


SELECT *
FROM orders_import_lines
WHERE raw_line ~
      '(<)?[A-Za-z0-9._%+-]+@[A-Za-z0-9-]+\\.[A-Za-z]{2,}(>)?';

SELECT *
FROM orders_import_lines
WHERE raw_line !~
      '(<)?[A-Za-z0-9._%+-]+@[A-Za-z0-9-]+\\.[A-Za-z]{2,}(>)?';

SELECT
    id,
    source_file,
    line_no,
    (regexp_match(
        raw_line,
        '(<)?([A-Za-z0-9._%+-]+@[A-Za-z0-9-]+\\.[A-Za-z]{2,})(>)?'
     ))[2] AS email
FROM orders_import_lines
WHERE raw_line ~
      '(<)?[A-Za-z0-9._%+-]+@[A-Za-z0-9-]+\\.[A-Za-z]{2,}';

SELECT
    id,
    source_file,
    line_no,
    m[1] AS sku
FROM orders_import_lines
CROSS JOIN LATERAL regexp_matches(
    raw_line,
    '\\b([A-Za-z]{1,3}-?[0-9]{1,4}(-[A-Za-z]{1,3})?)\\b',
    'g'
) AS m;

SELECT
    id,
    raw_line,
    (
      regexp_replace(
        regexp_replace(raw_line, '[^0-9,\\. ]', '', 'g'),
        '[ ,](?=\\d{3}(\\D|$))',
        '',
        'g'
      )
    )::numeric AS price_normalized
FROM orders_import_lines
WHERE raw_line ~ 'price';

SELECT
    id,
    regexp_split_to_array(
        regexp_replace(raw_line, '^.*tags:\\s*', ''),
        '\\s*,\\s*'
    ) AS tags
FROM orders_import_lines
WHERE raw_line ~ '^tags:';

SELECT
    id,
    regexp_split_to_table(
        raw_line,
        ',(?=(?:[^"]*"[^"]*")*[^"]*$)'
    ) AS field
FROM orders_import_lines
WHERE source_file = 'orders_dirty.csv';

SELECT *
FROM orders_import_lines
WHERE source_file = 'processor_log.txt'
  AND raw_line ~* '\\berror\\b';

SELECT
    id,
    regexp_replace(raw_line, '\\berror\\b', 'ERROR', 'gi') AS normalized_log
FROM orders_import_lines
WHERE source_file = 'processor_log.txt';
