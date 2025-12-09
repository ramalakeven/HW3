CREATE TABLE books (
    id SERIAL PRIMARY KEY,
    title TEXT NOT NULL,
    author TEXT NOT NULL,
    genre TEXT NOT NULL,
    price NUMERIC(10, 2) NOT NULL CHECK (price > 0),
    published_date DATE NOT NULL,

    CONSTRAINT chk_genre_not_empty CHECK (genre <> ''),
    CONSTRAINT chk_title_not_empty CHECK (title <> ''),
    CONSTRAINT chk_author_not_empty CHECK (author <> '')
);

INSERT INTO books (title, author, genre, price, published_date) VALUES
('Dragon Heart', 'A. Miller', 'Fantasy', 15.99, '2015-06-10'),
('Dragon Rising', 'L. Storm', 'Fantasy Adventure', 12.50, '2010-01-01'), 
('Dragonfall', 'M. Page', 'dark fantasy', 18.20, '2020-12-31'),
('Dragon of the North', 'K. Reed', 'Epic Fantasy', 9.99, '2009-12-31'), 
('Dragon Tales', 'R. Bloom', 'Children Fantasy', 14.00, '2021-01-01'), 
('Mystic Dragons', 'H. Brook', 'Mystery', 11.50, '2018-03-20'),

('Star Voyage', 'T. Gray', 'Science Fiction', 10.99, '2014-04-15'),
('Galactic Wars', 'Z. Kent', 'Science Fiction', 19.99, '2013-08-01'), 
('Nebula Dreams', 'W. Oliver', 'Science Fiction', 9.98, '2016-05-22'), 
('Quantum Box Set', 'E. Turner', 'Science Fiction', 15.00, '2017-07-09'),
('Cosmic Dawn', 'A. Nolan', 'Science Fiction', 20.00, '2012-12-12'), 
('Solar Drift', 'M. Young', 'Science fiction', 13.45, '2011-10-30'), 

('Sample Reference Guide', 'D. Collins', 'Reference', 22.00, '1995-03-12'), 
('Sample Index Book', 'U. Reed', 'Reference', 18.00, '1980-07-19'), 
('Reference Manual', 'T. West', 'Reference', 30.00, '2005-06-01'), 
('Sample Fiction Story', 'R. Nolan', 'Fiction', 12.00, '1990-05-05'), 
('Reference Sample Work', 'G. Hart', 'Reference', 28.00, '2000-01-01'),
('Old Reference Book', 'V. Pike', 'Reference', 17.00, '1999-12-31'),

('Romance in Paris', 'J. Hale', 'Romance', 8.99, '2011-02-14'),
('Dr.Acula', 'J. Dorian', 'Mystery', 13.99, '2008-11-30'),
('Wild Journey', 'K. Stone', 'Adventure', 16.50, '2019-09-01'),
('Mageborn', 'A. Winter', 'Fantasy', 11.20, '2003-03-03'),
('Sunset Dreams', 'C. Ivory', 'Drama', 9.50, '2018-10-10');

SELECT *
FROM books
WHERE genre ILIKE '%fantasy%'
  AND title ILIKE 'dragon%'
  AND published_date BETWEEN '2010-01-01' AND '2020-12-31'
ORDER BY title;

UPDATE books
SET price = price * 1.15
WHERE genre ILIKE 'science fiction'
  AND price BETWEEN 9.99 AND 19.99
  AND title NOT ILIKE '%box set%';

DELETE FROM books
WHERE genre = 'Reference'
  AND published_date < '2000-01-01'
  AND title ILIKE '%sample%';
