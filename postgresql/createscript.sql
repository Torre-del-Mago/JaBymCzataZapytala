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

CREATE TABLE created_diet (
	id SERIAL PRIMARY KEY,
	name VARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE created_hotel (
	id SERIAL PRIMARY KEY,
	name VARCHAR(50) NOT NULL,
	discount REAL NOT NULL
);

CREATE TABLE created_hotel_diet (
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

CREATE TABLE created_room_type (
	id SERIAL PRIMARY KEY,
	name VARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE created_hotel_room_type (
	id SERIAL PRIMARY KEY,
	hotel_id INT NOT NULL,
	room_type_id INT NOT NULL,
	number_of_rooms INT NOT NULL,
	price_per_night REAL NOT NULL
);

CREATE TABLE booked_reservation (
	id SERIAL PRIMARY KEY,
	hotel_room_type INT NOT NULL,
	from_date Date,
	to_date Date,
	number_of_rooms INT
)

CREATE TABLE canceled_reservation (
	id SERIAL PRIMARY KEY,
	reservation_id INT
)