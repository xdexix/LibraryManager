-- SQLite
CREATE TABLE AUTOR (
    ID          INTEGER         PRIMARY KEY,
    Name        TEXT,
    Surname     TEXT,
    Country     TEXT,
    Birth       TEXT
);

CREATE TABLE PUBLISHING (
    ID          INTEGER         PRIMARY KEY,
    Title       TEXT,
    Town        TEXT,
    Country     TEXT,
    Adress      TEXT
);

CREATE TABLE READER (
    ID          INTEGER         PRIMARY KEY,
    Name        TEXT,
    Surname     TEXT,
    Email       TEXT,
    Phone       TEXT,
    Adress      TEXT
);

CREATE TABLE LIBRARIAN (
    ID          INTEGER         PRIMARY KEY,
    Name        TEXT,
    Surname     TEXT,
    POST        TEXT
);

CREATE TABLE RENT (
    ID          INTEGER         PRIMARY KEY,
    Reader      INTEGER,
    Librarian   INTEGER,
    Status      INTEGER,
    Start_t     TEXT,
    End_t       TEXT,
    FOREIGN KEY (Librarian)     REFERENCES LIBRARIAN(ID),
    FOREIGN KEY (Reader)        REFERENCES READER(ID)
);

CREATE TABLE BOOK (
    ID          INTEGER         PRIMARY KEY,
    Autor       INTEGER,
    Publishing  INTEGER,
    Rent        INTEGER,
    Title       TEXT,
    Publish     TEXT,
    Genre       TEXT,
    FOREIGN KEY (Autor)         REFERENCES AUTOR(ID),
    FOREIGN KEY (Publishing)    REFERENCES PUBLISHING(ID), 
    FOREIGN KEY (Rent)          REFERENCES RENT(ID)
);