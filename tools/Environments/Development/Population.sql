USE HealthcareReimbursements;

GO

INSERT INTO Users (PublicId, RowVersionId, Name) VALUES
	('b8651236-f2c8-4ace-ad91-624e206ad790', 'cad0940b-afdd-4a71-a37a-946b3761ce81', 'John Doe');

GO

INSERT INTO UserEmailAddresses (PublicId, RowVersionId, UserId, EmailAddress) 
	SELECT	
		'c296b4af-c709-4549-ba8f-2c7b02fa9633' PublicId,
		'67162f69-46ac-4f8c-9d7a-38cb8430dd05' RowVersionId,
		Users.Id UserId,
		'john.doe@datapoint.software'
	FROM
		Users
	WHERE
		Users.PublicId = 'b8651236-f2c8-4ace-ad91-624e206ad790';

GO

INSERT INTO UserPasswords (PublicId, RowVersionId, UserId, Hash)
	SELECT 
		'78b3c995-6654-41bd-a961-4e9ccfada2c6' PublicId,
		'c18373f0-67b0-4bed-9ea8-31328957ad1b' RowVersionId,
		Users.Id,
		'$2a$08$fxCTDlmLkuXbywzmZmv51uTyZ.rkQ9Nws4L2CFfNe7lPm1YhsC1sy' Hash
	FROM
		Users
	WHERE
		Users.PublicId = 'b8651236-f2c8-4ace-ad91-624e206ad790';

GO