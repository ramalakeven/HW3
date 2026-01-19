DROP TABLE IF EXISTS users_perf;
CREATE TABLE users_perf (
    id SERIAL PRIMARY KEY,
    username TEXT NOT NULL,
    phone TEXT NOT NULL,
    city TEXT NOT NULL,
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

INSERT INTO users_perf (username, phone, city, created_at)
SELECT
    'user_' || gs,
    '+7' || (9000000000 + (random() * 999999999)::bigint),
    (ARRAY[
        'Москва',
        'Санкт-Петербург',
        'Казань',
        'Нижний Новгород',
        'Самара',
        'Новосибирск',
        'Екатеринбург',
        'Омск',
        'Тольятти',
        'Владивосток'
    ])[1 + floor(random() * 12)::int] AS city,
    NOW() - ((random() * 365)::int || ' days')::interval
FROM generate_series(1, 500000) AS g;

EXPLAIN ANALYZE
WITH mid AS (
    SELECT phone
    FROM users_perf
    ORDER BY id
    OFFSET (
        SELECT COUNT(*) / 2
        FROM users_perf
    )
    LIMIT 1
)
SELECT *
FROM users_perf
WHERE phone = '+79001234567';

CREATE INDEX idx_users_demo_phone
ON users_demo (phone);

EXPLAIN ANALYZE
SELECT *
FROM users_demo
WHERE phone = '+79001234567';

EXPLAIN ANALYZE
SELECT *
FROM users_demo
WHERE city ILIKE '%а%';

CREATE EXTENSION IF NOT EXISTS pg_trgm;
CREATE INDEX idx_users_demo_city_trgm
ON users_demo
USING GIN (city gin_trgm_ops);

EXPLAIN ANALYZE
SELECT *
FROM users_demo
WHERE city ILIKE '%а%';
