CREATE DATABASE `Reimbursements` 
    DEFAULT ENCRYPTION 'N'
	DEFAULT CHARACTER SET `utf8mb4`
    COLLATE `utf8mb4_0900_ai_ci`;

CREATE USER 'reimbursements-app'@'%'
IDENTIFIED BY 'c9e93853-8225-4ff7-b5e6-77fe222edd18';
    
GRANT DELETE, INSERT, UPDATE, SELECT
	ON `Reimbursements`.* 
	TO 'reimbursements-app'@'%';

FLUSH PRIVILEGES;

CREATE USER 'reimbursements-migrator-app'@'%'
	IDENTIFIED BY '667e9bd5-ffc1-4232-85ae-d085061668b4';
    
GRANT ALL PRIVILEGES
	ON `Reimbursements`.* 
	TO 'reimbursements-migrator-app'@'%';
    
FLUSH PRIVILEGES;