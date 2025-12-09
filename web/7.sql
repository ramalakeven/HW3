CREATE TABLE books (
    book_id SERIAL PRIMARY KEY,
    title TEXT NOT NULL,
    pages INTEGER NOT NULL CHECK (pages > 0),
    publication_year INTEGER NOT NULL CHECK (publication_year BETWEEN 1900 AND 2025)

     CONSTRAINT chk_pages_positive
        CHECK (pages > 0),

    CONSTRAINT chk_year_range
        CHECK (publication_year BETWEEN 1900 AND 2025)
);

INSERT INTO books (title, pages, publication_year) VALUES
    ('Мастер и Маргарита', 480, 1967),
    ('1984', 330, 1949),
    ('Пикник на обочине', 260, 1972),
    ('Гарри Поттер', 3500, 1997),
