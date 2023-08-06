USE HealthcareReimbursements;

GO

INSERT INTO Roles (PublicId, RowVersionId, Name, Description, Scope) VALUES
	('5863e7aa-8bc3-4567-ba2a-edf03340048e', 'e814d00a-171b-440f-a001-92d5b3c26c83', 'Administrador', 'Administradores do sistema de informação', ASCII('S')),
	('e39a5927-30ee-43bc-824a-719a328e713f', 'c4821fc7-fd91-4706-a34f-6610338d088f', 'Administrativo', 'Técnicos administrativos', ASCII('E')),
	('c679387c-1cb2-4afd-a78f-e9521d8a8f9c', 'f2d89735-c25f-4248-8298-805c4edbf2b3', 'Codificador', 'Técnicos de codificação', ASCII('E')),
	('897efbab-c6a0-457b-af0e-5687473cc7df', '79484b64-40c2-479b-b446-a2a52eb03129', 'Validator', 'Técnicos de validação', ASCII('E')),
	('28e8f698-019e-45b1-a726-4d3a546e7da1', 'd80f3f77-a79e-4c85-9dea-7bfc9d722837', 'Tesoureiro', 'Técnicos de tesouraria', ASCII('E')),
	('88426c8f-01a4-41b8-a4a6-d725c84659f9', '2892d3c8-5ded-4b9f-b7f9-51ec15f3eef9', 'Gestor de ficheiros', 'Gestores de ficheiros', ASCII('E'));

GO

INSERT INTO Entities (PublicId, RowVersionId, Code, Name, Kind) VALUES

	-- Administration Offices
	('582a4771-29f1-42d8-9bf7-5a2408f6a9f8', '6472b0ee-bb44-434b-ac41-0733044794fa', 'USIC', 'Unidade de Saúde de Ilha do Corvo', ASCII('A')),
	('4d8602ef-566f-41be-b297-f9c8055ffaf4', '8a956df9-bc7f-48d3-bd58-5154d0f1a46f', 'USIF', 'Unidade de Saúde de Ilha do Faial', ASCII('A')),
	('3b6b63a6-b591-4513-a664-1fa6d1ad995d', '928fb932-8d2a-4d29-be24-23645bde5860', 'USIFL', 'Unidade de Saúde de Ilha das Flores', ASCII('A')),
	('d6396def-94bb-4445-9a0e-89e2da2a05f1', '47a82830-cee5-4c26-b330-ec4ff6fda792', 'USIG', 'Unidade de Saúde de Ilha da Graciosa', ASCII('A')),
	('2a475f48-67ab-4469-b68f-09ba588d5f7f', 'a594f997-0dc9-416e-b71b-00420d43b7aa', 'USIP', 'Unidade de Saúde de Ilha do Pico', ASCII('A')),
	('dc7b8706-25a3-433e-9ddb-faf624377b5a', '11134d24-c6f2-421d-a634-084a9facce18', 'USISJ', 'Unidade de Saúde de Ilha de São Jorge', ASCII('A')),
	('2d2b73c4-e360-42ad-b17d-eaf1e49ba0ae', 'efe6ae28-8aa4-4ec3-afb9-6c5ce2cf2b6e', 'USISM', 'Unidade de Saúde de Ilha de Santa Maria', ASCII('A')),
	('4fa8bc15-d84d-4297-8d94-a1848a5e0756', '36c6cec9-343e-4b85-9ef9-71c3d5c04e2c', 'USISMG', 'Unidade de Saúde de Ilha de São Miguel', ASCII('A')),
	('05028ded-0de8-45c7-b257-d09397fd1f9f', 'f37a7175-53b0-40c3-8ea3-a87197d19079', 'USIT', 'Unidade de Saúde de Ilha da Terceira', ASCII('A')),
	
	-- Health Centers
	('ade50bda-a607-4647-8aff-713862c6744e', 'c14eeb6d-af53-4360-a516-3b3f0791bfdc', 'CSAH', 'Centro de Saúde de Angra do Heroísmo', ASCII('C')),
	('60c63a46-a714-465b-8f85-3fdadcd4d1ef', 'f117671b-c2da-42a6-8f2e-4ababe1edf43', 'CSCA', 'Centro de Saúde da Calheta', ASCII('C')),
	('5b0a0318-ef9a-4280-ad50-e0ce8090e00e', 'aa6ce8ac-d225-4e06-81d4-73eafb8eb691', 'CSCO', 'Centro de Saúde do Corvo', ASCII('C')),
	('0c0832c6-539d-4461-ad23-da515adf939d', '5c589d55-0680-41c7-b96f-4e0e28660edd', 'CSH', 'Centro de Saúde da Horta', ASCII('C')),
	('cbd0fba9-94ff-43fe-81f2-4957b1bce7f1', '799ecbef-ae03-44e5-ad7a-b85c8944d3fd', 'CSLP', 'Centro de Saúde das Lajes do Pico', ASCII('C')),
	('970f296d-e8bd-4668-a819-d7b17dc3e767', 'e4ae13d3-c967-45ca-b392-a9609ec6b9e5', 'CSM', 'Centro de Saúde da Madalena', ASCII('C')),
	('d7aefabd-2ae3-454f-abd4-14878cb43f8e', '8ee844c2-d48f-43cd-8c75-cbc94968de20', 'CSN', 'Centro de Saúde do Nordeste', ASCII('C')),
	('33219b90-3cee-4400-a667-42639364762a', 'a2b067b8-298d-460c-815e-a794d9950174', 'CSP', 'Centro de Saúde da Povoação', ASCII('C')),
	('76ee7d72-3cb1-40c6-91de-de9c6adeda6f', 'f48f8f1a-5d79-46c0-8545-721809edea5c', 'CSPD', 'Centro de Saúde de Ponta Delgada', ASCII('C')),
	('95cdac39-8bbb-4993-99eb-c4266c913403', '2ec188d3-216d-491f-abaa-b8b9c1daeac6', 'CSPV', 'Centro de Saúde da Praia da Vitória', ASCII('C')),
	('b2ca817c-c362-431e-9f52-754dfbbd99ae', 'a256492e-bb8b-4735-87af-8ad8987084a1', 'CSRG', 'Centro de Saúde da Ribeira Grande', ASCII('C')),
	('17d82ce8-121c-4a4f-a475-ba553ee00c05', 'e11c8609-7c38-4d96-ab2c-4aaa210ff6ac', 'CSSCF', 'Centro de Saúde de Santa Cruz das Flores', ASCII('C')),
	('7427bfea-3120-4ef7-8465-feff78d7fab4', '01ec4606-af3c-4bfd-8bc9-a13e36f4fbd8', 'CSSCG', 'Centro de Saúde de Santa Cruz da Graciosa', ASCII('C')),
	('adb3bc9d-1cc0-4427-9dd9-5034a367c2c2', 'ccdd2500-acf1-4d79-b3ad-fa92fa3f7c16', 'CSSR', 'Centro de Saúde de São Roque do Pico', ASCII('C')),
	('04b883f4-cf97-45d1-8276-52241a4ea8f8', 'a9b47ed0-8c55-4c00-88db-3c546174e064', 'CSV', 'Centro de Saúde das Velas', ASCII('C')),
	('0617c35f-5904-41cf-8288-5f40742bad35', 'f9fc2e4c-7ce3-4c96-9522-5c9040c4b826', 'CSVFC', 'Centro de Saúde de Vila Franca do Campo', ASCII('C')),
	('453c2057-7b7c-4d5f-9895-04e4f9a48bc7', '1098c2be-92b0-43d1-b3f3-0eb65c8a4d49', 'CSVP', 'Centro de Saúde de Vila do Porto', ASCII('C'));

GO

INSERT INTO EntityParents (PublicId, RowVersionId, EntityId, ParentEntityId, Level)
	
	-- USIC: CSCO
	SELECT NEWID(), NEWID(), e.Id, pe.Id, 1
	FROM Entities e, Entities pe
	WHERE pe.PublicId = '582a4771-29f1-42d8-9bf7-5a2408f6a9f8' AND
		e.PublicId IN ('5b0a0318-ef9a-4280-ad50-e0ce8090e00e')

	UNION
		
	-- USIF: CSH
	SELECT NEWID(), NEWID(), e.Id, pe.Id, 1
	FROM Entities e, Entities pe
	WHERE pe.PublicId = '4d8602ef-566f-41be-b297-f9c8055ffaf4' AND
		e.PublicId IN ('0c0832c6-539d-4461-ad23-da515adf939d')
		
	UNION
	
	-- USIFL: CSSCF
	SELECT NEWID(), NEWID(), e.Id, pe.Id, 1
	FROM Entities e, Entities pe
	WHERE pe.PublicId = '3b6b63a6-b591-4513-a664-1fa6d1ad995d' AND
		e.PublicId IN ('17d82ce8-121c-4a4f-a475-ba553ee00c05')
		
	UNION
	
	-- USIFL: CSSCF
	SELECT NEWID(), NEWID(), e.Id, pe.Id, 1
	FROM Entities e, Entities pe
	WHERE pe.PublicId = 'd6396def-94bb-4445-9a0e-89e2da2a05f1' AND
		e.PublicId IN ('7427bfea-3120-4ef7-8465-feff78d7fab4')
		
	UNION
	
	-- USIP: CSSM, CSSR, CSLP
	SELECT NEWID(), NEWID(), e.Id, pe.Id, 1
	FROM Entities e, Entities pe
	WHERE pe.PublicId = '2a475f48-67ab-4469-b68f-09ba588d5f7f' AND
		e.PublicId IN ('970f296d-e8bd-4668-a819-d7b17dc3e767', 'adb3bc9d-1cc0-4427-9dd9-5034a367c2c2', 'cbd0fba9-94ff-43fe-81f2-4957b1bce7f1')
		
	UNION
	
	-- USIP: CSCA, CSV
	SELECT NEWID(), NEWID(), e.Id, pe.Id, 1
	FROM Entities e, Entities pe
	WHERE pe.PublicId = 'dc7b8706-25a3-433e-9ddb-faf624377b5a' AND
		e.PublicId IN ('60c63a46-a714-465b-8f85-3fdadcd4d1ef', '04b883f4-cf97-45d1-8276-52241a4ea8f8')
		
	UNION
	
	-- USISM: CSVP
	SELECT NEWID(), NEWID(), e.Id, pe.Id, 1
	FROM Entities e, Entities pe
	WHERE pe.PublicId = '2d2b73c4-e360-42ad-b17d-eaf1e49ba0ae' AND
		e.PublicId IN ('453c2057-7b7c-4d5f-9895-04e4f9a48bc7')
		
	UNION
	
	-- USISMG: CSN, CSPD, CSP, CSRG, CSVFC
	SELECT NEWID(), NEWID(), e.Id, pe.Id, 1
	FROM Entities e, Entities pe
	WHERE pe.PublicId = '4fa8bc15-d84d-4297-8d94-a1848a5e0756' AND
		e.PublicId IN ('d7aefabd-2ae3-454f-abd4-14878cb43f8e', '76ee7d72-3cb1-40c6-91de-de9c6adeda6f', '33219b90-3cee-4400-a667-42639364762a', 'b2ca817c-c362-431e-9f52-754dfbbd99ae', '0617c35f-5904-41cf-8288-5f40742bad35')
		
	UNION
	
	-- USIT: CSAH, CSPV
	SELECT NEWID(), NEWID(), e.Id, pe.Id, 1
	FROM Entities e, Entities pe
	WHERE pe.PublicId = '05028ded-0de8-45c7-b257-d09397fd1f9f' AND
		e.PublicId IN ('ade50bda-a607-4647-8aff-713862c6744e', '95cdac39-8bbb-4993-99eb-c4266c913403');

GO

INSERT INTO Users (PublicId, RowVersionId, Name) VALUES
	('b8651236-f2c8-4ace-ad91-624e206ad790', 'cad0940b-afdd-4a71-a37a-946b3761ce81', 'Datapoint Software');

GO

INSERT INTO UserEmailAddresses (PublicId, RowVersionId, UserId, EmailAddress) 
	SELECT 'c296b4af-c709-4549-ba8f-2c7b02fa9633', 
		'67162f69-46ac-4f8c-9d7a-38cb8430dd05',
		u.Id,
		'hello@datapoint.software'
	FROM Users u
	WHERE u.PublicId = 'b8651236-f2c8-4ace-ad91-624e206ad790';

GO

INSERT INTO UserPasswords (PublicId, RowVersionId, UserId, Hash)
	SELECT '78b3c995-6654-41bd-a961-4e9ccfada2c6',
		'c18373f0-67b0-4bed-9ea8-31328957ad1b',
		u.Id,
		'$2a$08$fxCTDlmLkuXbywzmZmv51uTyZ.rkQ9Nws4L2CFfNe7lPm1YhsC1sy' -- "password"
	FROM Users u
	WHERE u.PublicId = 'b8651236-f2c8-4ace-ad91-624e206ad790';

GO

INSERT INTO UserRoles (PublicId, RowVersionId, UserId, RoleId)
	SELECT '78b3c995-6654-41bd-a961-4e9ccfada2c6', 'c18373f0-67b0-4bed-9ea8-31328957ad1b', u.Id, r.Id
	FROM Users u, Roles r
	WHERE u.PublicId = 'b8651236-f2c8-4ace-ad91-624e206ad790' AND
		r.PublicId = '5863e7aa-8bc3-4567-ba2a-edf03340048e';

GO

INSERT INTO UserEntities (PublicId, RowVersionId, UserId, EntityId)
	SELECT NEWID(), NEWID(), u.Id, e.Id
	FROM Users u, Entities e
	WHERE u.PublicId = 'b8651236-f2c8-4ace-ad91-624e206ad790' AND
		e.PublicId IN ('05028ded-0de8-45c7-b257-d09397fd1f9f', 'ade50bda-a607-4647-8aff-713862c6744e', '95cdac39-8bbb-4993-99eb-c4266c913403');

GO

INSERT INTO UserPermissions (PublicId, RowVersionId, UserId, PermissionId, Granted)
	SELECT NEWID(), NEWID(), u.Id, p.Id, 1
	FROM Users u, Permissions p
	WHERE u.PublicId = 'b8651236-f2c8-4ace-ad91-624e206ad790';