CREATE DATABASE dbCOf
USE dbCOf

CREATE TABLE Paciente (Codigo_paciente INTEGER PRIMARY KEY,
	Nombres VARCHAR(30),
	Apellidos VARCHAR(30),
	Cedula NVARCHAR(16),
	FechaNac DATE,
	Direccion NVARCHAR(50));


CREATE TABLE Telefono (ID_Telefono INTEGER PRIMARY KEY,
	Num_Telefono NVARCHAR(8),
	Company NVARCHAR(5),
	Codigo_paciente INT FOREIGN KEY REFERENCES Paciente(Codigo_paciente));


CREATE TABLE Medico (Codigo_medico INTEGER PRIMARY KEY,
	Nombres VARCHAR(30),
	Apellidos VARCHAR(30));


CREATE TABLE Especialidades (Codigo_Especialidad INTEGER PRIMARY KEY,
	Nombre VARCHAR(20),
	Codigo_medico INT FOREIGN KEY REFERENCES Medico(Codigo_medico));


CREATE TABLE Diagnostico (Codigo_diagnostico INTEGER PRIMARY KEY,
	Codigo_medico INT FOREIGN KEY REFERENCES Medico(Codigo_medico),
	Codigo_paciente INT FOREIGN KEY REFERENCES Paciente(Codigo_paciente),
	Valoracion_oftalmologica TEXT,
	ResultadosExa_Glucosa NVARCHAR(50),
	Valoracion_MedInterna TEXT,
	Valoracion_Anestesia TEXT,
	Fecha_diagnostico DATE,
	Notas_diagnostico TEXT);


CREATE TABLE Cirugias (Codigo_cirugia INTEGER PRIMARY KEY,
	Codigo_diagnostico INT FOREIGN KEY REFERENCES Diagnostico(Codigo_diagnostico),/*esta FK contiene el codigo del paciente y de los med. que hicieron valorac.*/
	Codigo_medico INT FOREIGN KEY REFERENCES Medico(Codigo_medico),/*esta FK son los medicos que haran la cirug.*/
	Fecha_cirugia DATE,
	Hora_inicio TIME,
	Hora_fin TIME);


CREATE TABLE Medicamentos (Codigo_Medicamento INTEGER PRIMARY KEY,
	Nombre_Medicamento NVARCHAR(20),
	Descripcion_medicamento TEXT);


CREATE TABLE Seguimientos_PostOperatorios (Codigo_seguimiento INTEGER PRIMARY KEY,
	Codigo_paciente INT FOREIGN KEY REFERENCES Paciente(Codigo_paciente),
	Codigo_Medicamento INT FOREIGN KEY REFERENCES Medicamentos(Codigo_Medicamento),
	Fecha_Control DATE,
	Programacion_ProximaCita DATE,
	Observaciones TEXT);


SELECT * FROM Paciente

