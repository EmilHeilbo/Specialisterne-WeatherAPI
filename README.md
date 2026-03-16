# ETL Intro

This programs purpose is to extract, transform and load data from external APIs to an internal database.

## Description
The program's main functionalities are:
* Extract weather data from the APIs at DMI and Specialisterne ApS.
* Transform it into generally uniform data structures (millicelsius to celsius etc.) 
* Load it into a custom-built SQL database "weather_db".

The program can run in docker, or it can create and write to a local Postgres database.
By design, the program is OS agnostics but has only been tested on Windows.

The work here was done as part of my course at Specialisterne ApS. As this was an exercise, little thought has been given to password security.



### Database structure
After initializing, the database will have 3 tables "DMI", "BME280" and "DS18B20" which stores data from each respective source. 
DMI and BME280 have observations of both temperature, humidity and pressure, while DS18B20 measures temperature only. 

The datatypes of the table columns are defined in load/schemas/table_schema.py. Here is an overview of the tables, with columns and datatypes:

**DMI Table:**

| Column       | Datatype                 | content                                                                                                 | 
|--------------|--------------------------|---------------------------------------------------------------------------------------------------------|
| id           | integer, primary key     | the database id                                                                                         |
| dmi_id       | UUID, foreign key        | the uuid of the reading in the dmi database                                                             |
| parameter_id | varchar(50)              | temperature, humidity or pressure                                                                       |
| value        | numeric(20,13)           | degree celsius for temperature, % for humidity and hPa for pressure                                     |
| observed_at  | timestamp with time zone | the date when the data was observed                                                                     |
| pulled_at    | timestamp with time zone | the date at which the data was pulled from the API                                                      |
| station_id   | integer                  | the dmi id for the station where the reading was made. 06181 - Jæbersborg; 06126 - Årslev; 06072 - Ødum |


**BME280 Table:**

| Column      | Datatype                 | content                                                    | 
|-------------|--------------------------|------------------------------------------------------------|
| id          | integer, primary key     | the database id                                            |
| reader_id   | UUID, foreign key        | the uuid of the reading in the dmi database                |
| location    | varchar(7) NOT NULL      | where the sensor is stored (must be 'inside' or 'outside') |
| humidity    | numeric(20,13)           | %                                                          |
| pressure    | numeric(20,13)           | hPa                                                        |
| temperature | numeric(20,13)           | celsius                                                    |
| observed_at | timestamp with time zone | the date when the data was observed                        |
| pulled_at   | timestamp with time zone | the date at which the data was pulled from the API         |

**DS18B20 Table:**

| Column      | Datatype                 | content                                                    | 
|-------------|--------------------------|------------------------------------------------------------|
| id          | integer, primary key     | the database id                                            |
| reader_id   | UUID, foreign key        | the uuid of the reading in the dmi database                |
| location    | varchar(7) NOT NULL      | where the sensor is stored (must be 'inside' or 'outside') |
| temperature | numeric(20,13)           | degree celsius                                             |
| observed_at | timestamp with time zone | the date when the data was observed                        |
| pulled_at   | timestamp with time zone | the date at which the data was pulled from the API         |



## Getting Started

### Dependencies

The dependencies hinge on whether you wish to run the program in docker or with a local postgreSQL database. 

If running in docker you will need:
* Docker desktop 

If running outside docker with a local database you will need:
* PostgreSQL
* The modules listed in requirements.txt


### Initial Set-up

* Download the app folder and main.py. Place them in the same directory.

If running in docker:
* download docker-compose.yml and Dockerfile. Place them next to main.py.

If running outside docker with a local database: 
* You likely need to edit the password in /load/schemas/database_schema.py. It should match your postgreSQL user password.
* Edit the variable 'docker' in main.py to 'False'. 


### Executing program

If running in Docker:
* Open docker desktop
* Navigate in terminal to the folder containing docker-compose.yml, Dockerfile and main.py
* On first run, run the following. The program will create a database with 3 tables ("DMI","BME280" and "DS18B20"). Then it extracts, transforms and loads data from the APis into the database.
```
docker-compose up --build
```
* Subsequently: If you need to access the database after closing, you can simply run
```
docker-compose up
```
* Now that the database is set up, you connect to the postgreSQL server by running
```
docker exec -it specialisternecase-etlpipeline-db-1 psql -U weather_app -d weather_db
```
* You can then run SQL queries in the command line. Example:
```
SELECT * FROM "DMI"
```
* To get a summary of the tables in the database, write
```
\dt
```
* To exit, first exit postgreSQL and then docker by writing
```
\q
docker-compose down
```

If running outside docker with a local database: 
* Run main.py - The program will create a database with 3 tables ("DMI","BME280" and "DS18B20"). Then it extracts, transforms and loads data from the APis into the database.
* Use your favorite method to query and view the data in the database (such as pgadmin4 or terminal).


### Resetting the database
If running in docker, you can run the following to reset the entire database.
```
docker-compose down -v
```
This will delete all persistent volumes.

Else, the program includes functionality for nuking tables or the database.
If you wish to reset the database, simply comment out line 14 and 15 in main.py or write
```
crud = CRUD()
crud.cleanse_db()
```
NB: This does not clear the internal ids of the tables. So, for example, if you have 145 rows in the "DMI" table with ids 1-145, the first row you insert will receive the internal id 146.

If you wish to delete the rows of a specific table, you can call the delete_all_rows method of the CRUD class. 
This method requires a table name as argument. For example, the following will delete all rows of the "DMI" table:
```
crud = CRUD()
crud.delete_all_rows("DMI")
```
NB: Note that as with the cleanse_db method, this does not clear the internal ids of the tables.

## Help

When running in docker, certain issues can be cleared by running the following in terminal:
```
docker-compose down -v
```
This will delete all persistent volumes.

## Authors

Me

## Version History

* 1.0 First fully functioning version with README