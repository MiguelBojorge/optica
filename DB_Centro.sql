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
    (2, 'Avenida Central', 'Le�n', 'Le�n'),
    (3, 'Colonia 10 de Junio', 'Masaya', 'Masaya'),
    (4, 'Barrio Guadalupe', 'Chinandega', 'Chinandega'),
    (5, 'Urbanizaci�n San Jos�', 'Estel�', 'Estel�'),
    (6, 'Barrio El Carmen', 'Matagalpa', 'Matagalpa'),
    (7, 'Colonia El Para�so', 'Granada', 'Granada'),
    (8, 'Reparto Santa Rosa', 'Jinotega', 'Jinotega'),
    (9, 'Barrio San Luis', 'Rivas', 'Rivas'),
    (10, 'Colonia La Florida', 'Carazo', 'Carazo');


-- Inserci�n de datos en la tabla Paciente
INSERT INTO Paciente (Codigo_paciente, Nombres, Apellidos, Cedula, FechaNac, Id_Direccion)
VALUES
    (1001, 'Mar�a', 'Lopez', '001-080455-0001U', '1955-04-08', 101),
    (1002, 'Carlos', 'Ramos', '002-150360-0002U', '1960-03-15', 2),
    (1003, 'Juana', 'Gonz�lez', '003-231248-0003U', '1948-12-23', 3),
    (1004, 'Francisco', 'Mart�nez', '004-290552-0004U', '1952-05-29', 4),
    (1005, 'Sof�a', 'Mej�a', '005-181165-0005U', '1965-11-18', 5),
    (1006, 'Jos�', 'P�rez', '006-030470-0006U', '1970-04-03', 6),
    (1007, 'Carmen', 'Vargas', '007-240968-0007U', '1968-09-24', 7),
    (1008, 'Ana', 'Castro', '008-050259-0008U', '1959-02-05', 8),
    (1009, 'Luis', 'Reyes', '009-130845-0009U', '1945-08-13', 9),
    (1010, 'Ver�nica', 'Rivera', '010-300650-0010U', '1950-06-30', 10);



-- Inserci�n de datos en la tabla Medico
INSERT INTO Medico (Codigo_medico, Nombres, Apellidos, Codigo_Especialidad)
VALUES
    (123, 'Juan', 'P�rez', 1),
    (234, 'Ana', 'Garc�a', 1),
    (345, 'Luis', 'Mart�nez', 3),
    (403, 'Marta', 'Rodr�guez', 2),
    (509, 'Carlos', 'Hern�ndez', 5),
    (634, 'Luc�a', 'L�pez', 2),
    (721, 'Pedro', 'Gonz�lez', 4),
    (890, 'Sara', 'Ram�rez', 4),
    (900, 'Manuel', 'S�nchez', 5),
    (101, 'Elena', 'Fern�ndez', 2);

-- Inserci�n de datos en la tabla Especialidades
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
-- Inserci�n de datos en la tabla Diagnostico
INSERT INTO Diagnostico (Codigo_medico, Codigo_paciente, Valoracion_oftalmologica, ResultadosExa_Glucosa, Valoracion_MedInterna, Valoracion_Anestesia, Fecha_diagnostico, Notas_diagnostico)
VALUES
    (123, 1001, 'Catarata avanzada en OD', 'Glucosa en ayunas: 115 mg/dL', 'Estable, sin complicaciones mayores', 'Apta para cirug�a con anestesia t�pica', '2023-07-15', 'Preparar cirug�a en centro oftalmol�gico de Managua'),
    (234, 1002, 'Miop�a y astigmatismo', 'Glucosa en ayunas: 90 mg/dL', 'Sin riesgos adicionales', 'Sin anestesia requerida', '2023-08-01', 'Recomendaci�n de gafas progresivas para visi�n cercana'),
    (345, 1003, 'Glaucoma de �ngulo abierto', 'Glucosa en ayunas: 105 mg/dL', 'Presi�n arterial controlada', 'Apta para procedimientos de seguimiento', '2023-08-20', 'Monitoreo y seguimiento cada tres meses en Masaya'),
    (403, 1004, 'Retinopat�a diab�tica leve', 'Glucosa en ayunas: 180 mg/dL', 'Control de diabetes', 'Apta para sedaci�n moderada', '2023-09-10', 'Evaluaci�n para tratamiento de retinopat�a en cl�nica especializada en Chinandega'),
    (509, 1005, 'Astigmatismo leve', 'Glucosa en ayunas: 98 mg/dL', 'Sin condiciones de riesgo', 'Sin anestesia necesaria', '2023-09-30', 'Requiere lentes correctivos para lectura'),
    (634, 1006, 'Presbicia y catarata incipiente', 'Glucosa en ayunas: 110 mg/dL', 'En buena condici�n general', 'Apta para anestesia local', '2023-10-15', 'Cirug�a de catarata programada en hospital de Matagalpa'),
    (721, 1007, 'Hipermetrop�a con opacidad leve', 'Glucosa en ayunas: 99 mg/dL', 'Controlado', 'Apta para procedimiento con sedaci�n leve', '2023-10-20', 'Evaluaci�n anual recomendada en Granada'),
    (890, 1008, 'Degeneraci�n macular', 'Glucosa en ayunas: 120 mg/dL', 'Presi�n controlada', 'Sin anestesia requerida', '2023-11-05', 'Inyecciones intrav�treas en hospital regional en Jinotega'),
    (900, 1009, 'Conjuntivitis al�rgica cr�nica', 'Glucosa en ayunas: 85 mg/dL', 'Sin complicaciones', 'No se requiere anestesia', '2023-11-10', 'Tratamiento con antihistam�nicos t�picos en Rivas'),
    (101, 1010, 'Catarata moderada en OI', 'Glucosa en ayunas: 95 mg/dL', 'Apto para cirug�a', 'Anestesia t�pica recomendada', '2023-11-15', 'Preparaci�n para cirug�a en cl�nica oftalmol�gica en Carazo');




-- Tel�fonos
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

-- Inserci�n de datos en la tabla Cirugias
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

-- Inserci�n de datos en la tabla Seguimientos_PostOperaciones
	INSERT INTO Seguimientos_PostOperatorios (Fecha_Control, Programacion_ProximaCita, Observaciones)
VALUES
    ('2023-07-28', '2023-08-28', 'Revisi�n postoperatoria. Sin complicaciones.'),
    ('2023-09-01', '2023-10-01', 'Revisi�n de adaptaci�n a lentes progresivos.'),
    ('2023-09-15', '2023-10-15', 'Contin�a tratamiento para control de presi�n intraocular.'),
    ('2023-09-20', '2023-10-20', 'Recomendaci�n de control de glucosa y retinopat�a.'),
    ('2023-10-01', '2023-11-01', 'Adaptaci�n adecuada a lentes. No hay molestias.'),
    ('2023-11-01', '2023-12-01', 'Planificaci�n de pr�xima cirug�a de catarata.'),
    ('2023-12-05', '2024-01-05', 'Revisi�n general de visi�n y control de opacidad leve.'),
    ('2024-01-05', '2024-02-05', 'Inyecci�n intrav�trea aplicada sin complicaciones.'),
    ('2024-01-10', '2024-02-10', 'Contin�a tratamiento con antihistam�nicos.'),
    ('2024-01-15', '2024-02-15', 'Satisfactoria recuperaci�n postoperatoria de catarata.');

Select * from Seguimientos_PostOperatorios

	-- Inserci�n de datos en la tabla
	INSERT INTO Medicamentos(Nombre_Medicamento, Descripcion_medicamento)
	VALUES
	('Timolol', 'Reductor de presi�n intraocular'),
	('Latanoprost','Medicamento para el glaucoma'),
	('Acetazolamida','Control de presi�n ocular'),
	('Ciprofloxacino','Antibi�tico en gotas para infecciones oculares'),
	('Atropina','Dilataci�n de pupilas para ex�menes'),
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

SELECT P.Nombres + ' ' + P.Apellidos AS 'Nombre y Apellido', C.Fecha_cirugia AS 'Realizaci�n cirugia', D.Fecha_diagnostico AS 'Realizaci�n de diagnostico'
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
