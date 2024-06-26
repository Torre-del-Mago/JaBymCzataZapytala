/*CREATE DATABASE local_database
    WITH
    OWNER = root
    ENCODING = 'UTF8'
    LC_COLLATE = 'en_US.utf8'
    LC_CTYPE = 'en_US.utf8'
    LOCALE_PROVIDER = 'libc'
    TABLESPACE = pg_default
    CONNECTION LIMIT = -1
    IS_TEMPLATE = False;*/

CREATE TABLE diet (
	id SERIAL PRIMARY KEY,
	name VARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE hotel (
	id SERIAL PRIMARY KEY,
	name VARCHAR(50) NOT NULL,
	discount REAL NOT NULL
);

CREATE TABLE hotel_diet (
	hotel_id INT NOT NULL,
	diet_id INT NOT NULL,
	PRIMARY KEY(hotel_id, diet_id)
	/*CONSTRAINT fk_hotel
	    FOREIGN KEY(hotel_id)
	    REFERENCES created_hotel(id),
	CONSTRAINT fk_diet
	    FOREIGN KEY(diet_id)
	    REFERENCES created_diet(id)*/
);

CREATE TABLE room_type (
	id SERIAL PRIMARY KEY,
	name VARCHAR(50) UNIQUE NOT NULL,
	number_of_people INT NOT NULL
);

CREATE TABLE hotel_room_type (
	id SERIAL PRIMARY KEY,
	hotel_id INT NOT NULL,
	room_type_id INT NOT NULL,
	number_of_rooms INT NOT NULL,
	price_per_night REAL NOT NULL
);

CREATE TABLE reservation (
	id int PRIMARY KEY,
	from_date Date NOT NULL,
	to_date Date NOT NULL,
	active boolean NOT NULL DEFAULT TRUE
);

CREATE TABLE reserved_rooms (
	id SERIAL PRIMARY KEY,
	reservation_id integer NOT NULL,
    hotel_room_type integer NOT NULL,
    number_of_rooms integer NOT NULL
);

CREATE TABLE booked_hotel_rooms(
	id SERIAL PRIMARY KEY,
	reservation_id integer NOT NULL,
    hotel_room_type integer NOT NULL,
    number_of_rooms integer NOT NULL
);

CREATE TABLE booked_reservation (
	id SERIAL PRIMARY KEY,
	from_date Date NOT NULL,
	to_date Date NOT NULL
);

CREATE TABLE canceled_reservation (
	id SERIAL PRIMARY KEY,
	reservation_id INT NOT NULL
);

CREATE TABLE canceled_reservation (
	id SERIAL PRIMARY KEY,
	reservation_id INT NOT NULL
)