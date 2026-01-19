
CREATE TABLE director (
    id      SERIAL PRIMARY KEY,
    name    TEXT NOT NULL UNIQUE,
    country TEXT
);

CREATE TABLE film (
    id                   SERIAL PRIMARY KEY,
    title                TEXT NOT NULL,
    release_year         INTEGER NOT NULL
        CHECK (release_year BETWEEN 1900 AND EXTRACT(YEAR FROM CURRENT_DATE)),
    primary_director_id  INTEGER NOT NULL
);


CREATE TABLE film_info (
    film_id          INTEGER PRIMARY KEY,
    duration_minutes INTEGER NOT NULL
        CHECK (duration_minutes > 0),
    rating           TEXT NOT NULL
        CHECK (rating IN ('G','PG','PG-13','R','NC-17')),
    budget_usd       NUMERIC(15, 2) NOT NULL
);


CREATE TABLE film_credit (
    film_id     INTEGER NOT NULL,
    director_id INTEGER NOT NULL,
    role        TEXT NOT NULL,
    PRIMARY KEY (film_id, director_id, role)
);



ALTER TABLE film
    ADD CONSTRAINT film_primary_director_fk
        FOREIGN KEY (primary_director_id)
        REFERENCES director (id)
        ON DELETE RESTRICT;



ALTER TABLE film_info
    ADD CONSTRAINT film_info_film_fk
        FOREIGN KEY (film_id)
        REFERENCES film (id)
        ON DELETE CASCADE;



ALTER TABLE film_credit
    ADD CONSTRAINT film_credit_film_fk
        FOREIGN KEY (film_id)
        REFERENCES film (id)
        ON DELETE CASCADE;

ALTER TABLE film_credit
    ADD CONSTRAINT film_credit_director_fk
        FOREIGN KEY (director_id)
        REFERENCES director (id)
        ON DELETE CASCADE;

INSERT INTO director (name, country) VALUES
('Christopher Nolan', 'UK'),
('Quentin Tarantino', 'USA');

INSERT INTO film (title, release_year, primary_director_id) VALUES
('Inception', 2010, 1),
('Pulp Fiction', 1994, 2);

INSERT INTO film_info (film_id, duration_minutes, rating, budget_usd) VALUES
(1, 148, 'PG-13', 160000000),
(2, 154, 'R', 8000000);

INSERT INTO film_credit (film_id, director_id, role) VALUES
(1, 1, 'director'),
(2, 2, 'director');

