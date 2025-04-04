CREATE DATABASE DB_CENTRO
USE DB_CENTRO

DROP DATABASE DBCENTRO

CREATE TABLE Paciente (Codigo_paciente INTEGER PRIMARY KEY,
	Nombres VARCHAR(30),
	Apellidos VARCHAR(30),
	Cedula NVARCHAR(16),
	FechaNac DATE,
	Id_Direccion INT FOREIGN KEY REFERENCES Direccion(Id_Direccion));


CREATE TABLE Direccion (Id_Direccion INTEGER PRIMARY KEY,
						Direccion_Domicilio VARCHAR(60),
						Ciudad VARCHAR(25),
						Departamentos VARCHAR(25)
						);


CREATE TABLE Telefono (ID_Telefono INTEGER PRIMARY KEY,
	Num_Telefono NVARCHAR(8),
	Company NVARCHAR(5),
	Codigo_paciente INT FOREIGN KEY REFERENCES Paciente(Codigo_paciente));


CREATE TABLE Medico (
    Codigo_medico INTEGER PRIMARY KEY,
    Nombres VARCHAR(30),
    Apellidos VARCHAR(30),
    Codigo_Especialidad INT,
    FOREIGN KEY (Codigo_Especialidad) REFERENCES Especialidades(Codigo_Especialidad)
);



CREATE TABLE Especialidades (Codigo_Especialidad INTEGER PRIMARY KEY,
	Nombre VARCHAR(25),
	);


CREATE TABLE Diagnostico (Codigo_diagnostico INT IDENTITY(1,1) PRIMARY KEY,
	Codigo_medico INT FOREIGN KEY REFERENCES Medico(Codigo_medico),
	Codigo_paciente INT FOREIGN KEY REFERENCES Paciente(Codigo_paciente),
	Valoracion_oftalmologica TEXT,
	ResultadosExa_Glucosa NVARCHAR(50),
	Valoracion_MedInterna TEXT,
	Valoracion_Anestesia TEXT,
	Fecha_diagnostico DATE,
	Notas_diagnostico TEXT);


CREATE TABLE Cirugias (Codigo_cirugia INT IDENTITY(1,1) PRIMARY KEY,
	Codigo_diagnostico INT FOREIGN KEY REFERENCES Diagnostico(Codigo_diagnostico),/*esta FK contiene el codigo del paciente y de los med. que hicieron valorac.*/
	Codigo_medico INT FOREIGN KEY REFERENCES Medico(Codigo_medico),/*esta FK son los medicos que haran la cirug.*/
	Fecha_cirugia DATE,
	Hora_inicio TIME,
	Hora_fin TIME);


CREATE TABLE Medicamentos (Codigo_Medicamento INT IDENTITY(1,1) PRIMARY KEY,
	Nombre_Medicamento NVARCHAR(20),
	Descripcion_medicamento TEXT);


CREATE TABLE Seguimientos_PostOperatorios (Codigo_seguimiento INT IDENTITY(1,1) PRIMARY KEY,
	Codigo_medico INT FOREIGN KEY REFERENCES Medico(Codigo_medico),
	Codigo_paciente INT FOREIGN KEY REFERENCES Paciente(Codigo_paciente),
	Codigo_Medicamento INT FOREIGN KEY REFERENCES Medicamentos(Codigo_Medicamento),
	Fecha_Control DATE,
	Programacion_ProximaCita DATE,
	Observaciones TEXT);


Drop table Medicamentos
Drop table Seguimientos_PostOperatorios


SELECT Paciente.Codigo_Paciente, Paciente.Nombres, Paciente.Apellidos, Paciente.Cedula, Paciente.FechaNac, Direccion.Id_Direccion, Direccion.Ciudad, Direccion.Departamentos, Telefono.ID_Telefono, Telefono.Num_Telefono, Telefono.Company 
FROM Direccion
JOIN Paciente
ON Direccion.Id_Direccion = Paciente.Id_Direccion
JOIN Telefono
ON Paciente.Codigo_paciente = Telefono.Codigo_paciente 


-----------------------------------------------------
SELECT Direccion.Id_Direccion, Direccion.Direccion_Domicilio,Direccion.Departamentos, Paciente.Nombres, Paciente.Apellidos
FROM Direccion
JOIN Paciente
ON Direccion.Id_Direccion = Paciente.Id_Direccion




-- Inserci�n de datos en la tabla Direccion
INSERT INTO Direccion (Id_Direccion, Direccion_Domicilio, Ciudad, Departamentos)
VALUES
    (101, 'Barrio Santa Ana, Calle Principal', 'Managua', 'Managua'),
    (2, 'Avenida Central', 'León', 'León'),
    (3, 'Colonia 10 de Junio', 'Masaya', 'Masaya'),
    (4, 'Barrio Guadalupe', 'Chinandega', 'Chinandega'),
    (5, 'Urbanización San José', 'Estelí', 'Estelí'),
    (6, 'Barrio El Carmen', 'Matagalpa', 'Matagalpa'),
    (7, 'Colonia El Paraíso', 'Granada', 'Granada'),
    (8, 'Reparto Santa Rosa', 'Jinotega', 'Jinotega'),
    (9, 'Barrio San Luis', 'Rivas', 'Rivas'),
    (10, 'Colonia La Florida', 'Carazo', 'Carazo');


-- Inserci�n de datos en la tabla Paciente
INSERT INTO Paciente (Codigo_paciente, Nombres, Apellidos, Cedula, FechaNac, Id_Direccion)
VALUES
    (1001, 'María', 'Lopez', '001-080455-0001U', '1955-04-08', 101),
    (1002, 'Carlos', 'Ramos', '002-150360-0002U', '1960-03-15', 2),
    (1003, 'Juana', 'González', '003-231248-0003U', '1948-12-23', 3),
    (1004, 'Francisco', 'Martínez', '004-290552-0004U', '1952-05-29', 4),
    (1005, 'Sofía', 'Mejía', '005-181165-0005U', '1965-11-18', 5),
    (1006, 'José', 'Pérez', '006-030470-0006U', '1970-04-03', 6),
    (1007, 'Carmen', 'Vargas', '007-240968-0007U', '1968-09-24', 7),
    (1008, 'Ana', 'Castro', '008-050259-0008U', '1959-02-05', 8),
    (1009, 'Luis', 'Reyes', '009-130845-0009U', '1945-08-13', 9),
    (1010, 'Verónica', 'Rivera', '010-300650-0010U', '1950-06-30', 10);



-- Inserción de datos en la tabla Medico
INSERT INTO Medico (Codigo_medico, Nombres, Apellidos, Codigo_Especialidad)
VALUES
    (123, 'Juan', 'Pérez', 1),
    (234, 'Ana', 'García', 1),
    (345, 'Luis', 'Martínez', 3),
    (403, 'Marta', 'Rodríguez', 2),
    (509, 'Carlos', 'Hernández', 5),
    (634, 'Lucía', 'López', 2),
    (721, 'Pedro', 'González', 4),
    (890, 'Sara', 'Ramírez', 4),
    (900, 'Manuel', 'Sánchez', 5),
    (101, 'Elena', 'Fernández', 2);

-- Inserción de datos en la tabla Especialidades
INSERT INTO Especialidades (Codigo_Especialidad, Nombre)
VALUES
    (1, 'Medicina de laboratorio'),
    (2, 'Anesteciologia'),
    (3, 'Oftalmologia'),
    (4, 'Medicina interna'),
    (5, 'Optometrista');



SELECT Medico.Codigo_medico, Medico.Nombres, Medico.Apellidos, Especialidades.Nombre 
FROM Medico
JOIN Especialidades
ON Medico.Codigo_Especialidad = Especialidades.Codigo_Especialidad

SELECT * FROM Medico


--ingresando datos de diagnostico
-- Inserción de datos en la tabla Diagnostico
INSERT INTO Diagnostico (Codigo_medico, Codigo_paciente, Valoracion_oftalmologica, ResultadosExa_Glucosa, Valoracion_MedInterna, Valoracion_Anestesia, Fecha_diagnostico, Notas_diagnostico)
VALUES
    (123, 1001, 'Catarata avanzada en OD', 'Glucosa en ayunas: 115 mg/dL', 'Estable, sin complicaciones mayores', 'Apta para cirugía con anestesia tópica', '2023-07-15', 'Preparar cirugía en centro oftalmológico de Managua'),
    (234, 1002, 'Miopía y astigmatismo', 'Glucosa en ayunas: 90 mg/dL', 'Sin riesgos adicionales', 'Sin anestesia requerida', '2023-08-01', 'Recomendación de gafas progresivas para visión cercana'),
    (345, 1003, 'Glaucoma de ángulo abierto', 'Glucosa en ayunas: 105 mg/dL', 'Presión arterial controlada', 'Apta para procedimientos de seguimiento', '2023-08-20', 'Monitoreo y seguimiento cada tres meses en Masaya'),
    (403, 1004, 'Retinopatía diabética leve', 'Glucosa en ayunas: 180 mg/dL', 'Control de diabetes', 'Apta para sedación moderada', '2023-09-10', 'Evaluación para tratamiento de retinopatía en clínica especializada en Chinandega'),
    (509, 1005, 'Astigmatismo leve', 'Glucosa en ayunas: 98 mg/dL', 'Sin condiciones de riesgo', 'Sin anestesia necesaria', '2023-09-30', 'Requiere lentes correctivos para lectura'),
    (634, 1006, 'Presbicia y catarata incipiente', 'Glucosa en ayunas: 110 mg/dL', 'En buena condición general', 'Apta para anestesia local', '2023-10-15', 'Cirugía de catarata programada en hospital de Matagalpa'),
    (721, 1007, 'Hipermetropía con opacidad leve', 'Glucosa en ayunas: 99 mg/dL', 'Controlado', 'Apta para procedimiento con sedación leve', '2023-10-20', 'Evaluación anual recomendada en Granada'),
    (890, 1008, 'Degeneración macular', 'Glucosa en ayunas: 120 mg/dL', 'Presión controlada', 'Sin anestesia requerida', '2023-11-05', 'Inyecciones intravítreas en hospital regional en Jinotega'),
    (900, 1009, 'Conjuntivitis alérgica crónica', 'Glucosa en ayunas: 85 mg/dL', 'Sin complicaciones', 'No se requiere anestesia', '2023-11-10', 'Tratamiento con antihistamínicos tópicos en Rivas'),
    (101, 1010, 'Catarata moderada en OI', 'Glucosa en ayunas: 95 mg/dL', 'Apto para cirugía', 'Anestesia tópica recomendada', '2023-11-15', 'Preparación para cirugía en clínica oftalmológica en Carazo');




-- Teléfonos
INSERT INTO Telefono (ID_Telefono, Num_Telefono, Company, Codigo_paciente)
VALUES
    (1, '88881234', 'Claro', 1001),
    (2, '87654321', 'Tigo', 1002),
    (3, '83563412', 'Claro', 1003),
    (4, '89765432', 'Tigo', 1004),
    (5, '85274123', 'Claro', 1005),
    (6, '80987654', 'Tigo', 1006),
	(7, '83563412', 'Claro', 1007),
    (8, '89765432', 'Tigo', 1008),
    (9, '85274123', 'Claro', 1009),
    (10, '80987654', 'Tigo', 1010)
	;


SELECT * 
FROM Paciente


SELECT * 
FROM Diagnostico

-- Inserción de datos en la tabla Cirugias
INSERT INTO Cirugias (Codigo_medico, Fecha_cirugia, Hora_inicio, Hora_fin)
VALUES
    (123, '2023-07-25', '08:30:00', '09:30:00'),
    (345, '2023-08-25', '10:00:00', '11:00:00'),
    (403, '2023-09-15', '09:00:00', '10:30:00'),
    (634, '2023-10-18', '11:00:00', '12:15:00'),
    (101, '2023-11-20', '14:00:00', '15:30:00'),
    (234, '2023-11-25', '13:00:00', '14:00:00'),
    (509, '2023-12-05', '09:30:00', '10:30:00'),
    (721, '2023-12-10', '11:30:00', '12:30:00'),
    (890, '2023-12-15', '15:00:00', '16:00:00'),
    (900, '2023-12-20', '10:00:00', '11:00:00');

-- Inserción de datos en la tabla Seguimientos_PostOperaciones
	INSERT INTO Seguimientos_PostOperatorios (Fecha_Control, Programacion_ProximaCita, Observaciones)
VALUES
    ('2023-07-28', '2023-08-28', 'Revisión postoperatoria. Sin complicaciones.'),
    ('2023-09-01', '2023-10-01', 'Revisión de adaptaci�n a lentes progresivos.'),
    ('2023-09-15', '2023-10-15', 'Continúa tratamiento para control de presión intraocular.'),
    ('2023-09-20', '2023-10-20', 'Recomendación de control de glucosa y retinopatía.'),
    ('2023-10-01', '2023-11-01', 'Adaptación adecuada a lentes. No hay molestias.'),
    ('2023-11-01', '2023-12-01', 'Planificación de próxima cirugía de catarata.'),
    ('2023-12-05', '2024-01-05', 'Revisión general de visión y control de opacidad leve.'),
    ('2024-01-05', '2024-02-05', 'Inyección intravítrea aplicada sin complicaciones.'),
    ('2024-01-10', '2024-02-10', 'Continúa tratamiento con antihistamínicos.'),
    ('2024-01-15', '2024-02-15', 'Satisfactoria recuperación postoperatoria de catarata.');

Select * from Seguimientos_PostOperatorios

	-- Inserción de datos en la tabla
	INSERT INTO Medicamentos(Nombre_Medicamento, Descripcion_medicamento)
	VALUES
	('Timolol', 'Reductor de presión intraocular'),
	('Latanoprost','Medicamento para el glaucoma'),
	('Acetazolamida','Control de presión ocular'),
	('Ciprofloxacino','Antibiótico en gotas para infecciones oculares'),
	('Atropina','Dilatación de pupilas para exámenes'),
	('Prednisolona','Anti-inflamatorio en gotas');
	
	DROP TABLE Especialidades

SELECT Paciente.Nombres + ' ' + Paciente.Apellidos AS 'Nombre y apellido', Medicamentos.Nombre_Medicamento, Seguimientos_PostOperatorios.Observaciones
FROM Paciente
JOIN Seguimientos_PostOperatorios
ON Seguimientos_PostOperatorios.Codigo_paciente = Paciente.Codigo_paciente
JOIN Medicamentos
ON Medicamentos.Codigo_Medicamento = Seguimientos_PostOperatorios.Codigo_Medicamento
WHERE Medicamentos.Nombre_Medicamento = 'Atropina'

SELECT M.Nombres + ' ' + M.Apellidos AS 'Nombre y Apellido', d.*
FROM Medico M
JOIN Diagnostico d
ON M.Codigo_medico = d.Codigo_medico
WHERE MONTH(d.Fecha_diagnostico) < 12 AND MONTH(d.Fecha_diagnostico) > 4 AND YEAR(d.Fecha_diagnostico) = 2024

SELECT P.Nombres + ' ' + P.Apellidos AS 'Nombre y Apellido', C.Fecha_cirugia AS 'Realización cirugia', D.Fecha_diagnostico AS 'Realización de diagnostico'
FROM Paciente P
JOIN Diagnostico D
ON D.Codigo_paciente = P.Codigo_paciente
JOIN Cirugias C
ON C.Codigo_diagnostico = D.Codigo_diagnostico
JOIN Direccion
ON Direccion.Id_Direccion = P.Id_Direccion
WHERE Direccion.Departamentos = 'Managua'


SELECT p.Codigo_paciente, p.Nombres, p.Apellidos, d.Codigo_diagnostico, d.Valoracion_oftalmologica, d.Valoracion_Anestesia
FROM Paciente p
JOIN Diagnostico d ON p.Codigo_paciente = d.Codigo_paciente
LEFT JOIN Cirugias c ON d.Codigo_diagnostico = c.Codigo_diagnostico
WHERE d.Valoracion_Anestesia LIKE '%Apta%'
  AND c.Codigo_cirugia IS NULL;
