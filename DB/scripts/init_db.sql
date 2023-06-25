USE ecommerce;

CREATE TABLE AspNetRoles (
    Id VARCHAR(255) NOT NULL,
    Name TEXT NULL,
    NormalizedName VARCHAR(255) NULL,
    ConcurrencyStamp TEXT NULL,
    PRIMARY KEY (Id)
);

INSERT INTO AspNetRoles VALUES ('23fa8aff-bc76-4897-94f9-917b15266ad4', 'cliente', 'CLIENTE', NULL);
INSERT INTO AspNetRoles VALUES ('7395c0f9-ee98-4d06-bb22-ee93106d6de4', 'admin', 'ADMIN', NULL);

CREATE TABLE AspNetRoleClaims (
    Id INT NOT NULL AUTO_INCREMENT,
    RoleId VARCHAR(255) NOT NULL,
    ClaimType TEXT NULL,
    ClaimValue TEXT NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY (RoleId) REFERENCES AspNetRoles (Id) ON DELETE CASCADE
);

CREATE TABLE AspNetUsers (
    Id VARCHAR(255) NOT NULL,
    Nome TEXT NOT NULL,
    UserName TEXT NULL,
    NormalizedUserName VARCHAR(255) NULL,
    Email TEXT NULL,
    NormalizedEmail VARCHAR(255) NULL,
    EmailConfirmed INT NOT NULL,
    PasswordHash TEXT NULL,
    SecurityStamp TEXT NULL,
    ConcurrencyStamp TEXT NULL,
    PhoneNumber TEXT NULL,
    PhoneNumberConfirmed INT NOT NULL,
    TwoFactorEnabled INT NOT NULL,
    LockoutEnd TEXT NULL,
    LockoutEnabled INT NOT NULL,
    AccessFailedCount INT NOT NULL,
    PRIMARY KEY (Id)
);

INSERT INTO AspNetUsers VALUES ('11a6d30f-5f31-4a11-9a8a-c58becba335a', 'ana diel', 'anadielkotz@gmail.com', 'ANADIELKOTZ@GMAIL.COM', 'anadielkotz@gmail.com', 'ANADIELKOTZ@GMAIL.COM', 0, 'AQAAAAIAAYagAAAAECbubTbIsHzGOGGwLcfuIOR0so8/s35S0b0rA7fHBaAu4Uv9Ll7WXsjyJ3smLq5bsQ==', 'ZEAWOAJQFFPAUYMNGEXOD2IPHF7KFIIV', 'cf461e21-7860-4bc6-827f-4c9dfa6a5ba9', '45999843802', 0, 0, NULL, 1, 0);

CREATE TABLE AspNetUserClaims (
    Id INT NOT NULL AUTO_INCREMENT,
    UserId VARCHAR(255) NOT NULL,
    ClaimType TEXT NULL,
    ClaimValue TEXT NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id) ON DELETE CASCADE
);

CREATE TABLE AspNetUserLogins (
    LoginProvider VARCHAR(255) NOT NULL,
    ProviderKey VARCHAR(255) NOT NULL,
    ProviderDisplayName TEXT NULL,
    UserId VARCHAR(255) NOT NULL,
    PRIMARY KEY (LoginProvider, ProviderKey),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id) ON DELETE CASCADE
);

CREATE TABLE AspNetUserRoles (
    UserId VARCHAR(255) NOT NULL,
    RoleId VARCHAR(255) NOT NULL,
    PRIMARY KEY (UserId, RoleId),
    FOREIGN KEY (RoleId) REFERENCES AspNetRoles (Id) ON DELETE CASCADE,
    FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id) ON DELETE CASCADE
);

CREATE TABLE AspNetUserTokens (
    UserId VARCHAR(255) NOT NULL,
    LoginProvider VARCHAR(255) NOT NULL,
    Name VARCHAR(255) NOT NULL,
    Value TEXT NULL,
    PRIMARY KEY (UserId, LoginProvider, Name),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers (Id) ON DELETE CASCADE
);

INSERT INTO AspNetUserRoles VALUES ('11a6d30f-5f31-4a11-9a8a-c58becba335a', '23fa8aff-bc76-4897-94f9-917b15266ad4');
INSERT INTO AspNetUserRoles VALUES ('11a6d30f-5f31-4a11-9a8a-c58becba335a', '7395c0f9-ee98-4d06-bb22-ee93106d6de4');
CREATE TABLE Cliente (
    IdCliente INT NOT NULL AUTO_INCREMENT,
    Nome TEXT NOT NULL,
    DataNascimento TEXT NOT NULL,
    CPF TEXT NOT NULL,
    Telefone TEXT NOT NULL,
    Email TEXT NOT NULL,
    Situacao INT NOT NULL,
    Endereco_Logradouro TEXT NULL,
    Endereco_Numero TEXT NULL,
    Endereco_Complemento TEXT NULL,
    Endereco_Bairro TEXT NULL,
    Endereco_Cidade TEXT NULL,
    Endereco_Estado TEXT NULL,
    Endereco_CEP TEXT NULL,
    Endereco_Referencia TEXT NULL,
    PRIMARY KEY (IdCliente)
);

INSERT INTO Cliente VALUES (1, 'ana diel', '2001-04-27', '12345678912', '45999843802', 'anadielkotz@gmail.com', 1, 'rua amazonas', '4111', NULL, 'parque independencia', 'medianeira', 'pr', '85884000', NULL);


CREATE TABLE Pedido (
    IdPedido INT NOT NULL AUTO_INCREMENT,
    DataHoraPedido DATETIME NOT NULL,
    ValorTotal REAL NOT NULL,
    Situacao INT NOT NULL,
    IdCliente INT NOT NULL,
    IdCarrinho TEXT NOT NULL,
    Endereco_Logradouro TEXT NULL,
    Endereco_Numero TEXT NULL,
    Endereco_Complemento TEXT NULL,
    Endereco_Bairro TEXT NULL,
    Endereco_Cidade TEXT NULL,
    Endereco_Estado TEXT NULL,
    Endereco_CEP TEXT NULL,
    Endereco_Referencia TEXT NULL,
    PRIMARY KEY (IdPedido),
    FOREIGN KEY (IdCliente) REFERENCES Cliente (IdCliente) ON DELETE RESTRICT
);

INSERT INTO Pedido VALUES (1, '2023-06-25 04:51:49', 608.0, 0, 1, 'b6087b56-aee5-46cb-8026-e363f01263ee', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO Pedido VALUES (2, '2023-06-25 05:31:38', 32.0, 0, 1, 'b785fc30-1270-45c8-b033-03534b7e45e9', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO Pedido VALUES (3, '2023-06-25 05:32:23', 32.0, 0, 1, '3cef61c3-8a28-4399-b1ea-2013b6789f17', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
INSERT INTO Pedido VALUES (4, '2023-06-25 17:01:22', 128.0, 1, 1, '6c9e0805-75b9-45bb-ac6b-2a481a41416b', 'rua amazonas', '4111', NULL, 'parque independencia', 'medianeira', 'pr', '85884000', NULL);
INSERT INTO Pedido VALUES (5, '2023-06-25 17:04:03', 32.0, 1, 1, '3477cf16-65d2-47c3-bbcb-fb9984e84f78', 'rua amazonas', '4111', NULL, 'parque independencia', 'medianeira', 'pr', '85884000', NULL);
INSERT INTO Pedido VALUES (6, '2023-06-25 17:09:44', 352.0, 1, 1, '6aac6cde-dc83-4500-a5aa-61a39e2f65ba', 'rua amazonas', '4111', NULL, 'parque independencia', 'medianeira', 'pr', '85884000', NULL);

CREATE TABLE Produto (
    IdProduto INT NOT NULL AUTO_INCREMENT,
    Nome TEXT NOT NULL,
    Descricao TEXT NOT NULL,
    Preco REAL NOT NULL,
    Estoque INT NOT NULL,
    PRIMARY KEY (IdProduto)
);

INSERT INTO Produto VALUES(3,'Kit Shampoo e Condicionador','Kit Shampoo e Condicionador',109.9,10);
INSERT INTO Produto VALUES(4,'Paleta de Sombras','Paleta de Sombras',59.9,10);
INSERT INTO Produto VALUES(5,'Base 30 FPS - Cor M�dia','Base 30 FPS - Cor M�dia',59.9,10);
INSERT INTO Produto VALUES(6,'Protetor Solar','Protetor Solar',39.9,5);
INSERT INTO Produto VALUES(7,'Base 30 FPS - Cor Clara','Base 30 FPS - Cor Clara',59.9,20);
INSERT INTO Produto VALUES(8,'Kit Shampoo e Condicionador','Kit Shampoo e Condicionador',109.9,15);
INSERT INTO Produto VALUES(9,'Paleta de Sombras','Paleta de Sombras',49.9,30);
INSERT INTO Produto VALUES(10,'Protetor Solar','Protetor Solar',59.9,10);
INSERT INTO Produto VALUES(11,'Kit Shampoo e Condicionador','Kit Shampoo e Condicionador',109.9,10);
INSERT INTO Produto VALUES(12,'Paleta de Sombras','Paleta de Sombras',49.9,25);
INSERT INTO Produto VALUES(13,'Base','Base',39.9,69);
INSERT INTO Produto VALUES(14,'Protetor Solar 50 FPS','Protetor Solar 50 FPS',59.9,78);
INSERT INTO Produto VALUES(15,'Paleta de Sombras','Protetor Solar 50 FPS',79.0,1);

CREATE TABLE ItemPedido (
    IdPedido INT NOT NULL,
    IdProduto INT NOT NULL,
    Quantidade REAL NOT NULL,
    ValorUnitario REAL NOT NULL,
    PRIMARY KEY (IdPedido, IdProduto),
    FOREIGN KEY (IdPedido) REFERENCES Pedido (IdPedido) ON DELETE CASCADE,
    FOREIGN KEY (IdProduto) REFERENCES Produto (IdProduto) ON DELETE RESTRICT
);

INSERT INTO ItemPedido VALUES (1, 1, 38.0, 16.0);
INSERT INTO ItemPedido VALUES (2, 1, 2.0, 16.0);
INSERT INTO ItemPedido VALUES (3, 1, 2.0, 16.0);
INSERT INTO ItemPedido VALUES (4, 1, 8.0, 16.0);
INSERT INTO ItemPedido VALUES (5, 1, 2.0, 16.0);
INSERT INTO ItemPedido VALUES (6, 1, 22.0, 16.0);

CREATE TABLE __EFMigrationsHistory (
    MigrationId VARCHAR(255) NOT NULL,
    ProductVersion TEXT NOT NULL,
    PRIMARY KEY (MigrationId)
);

INSERT INTO __EFMigrationsHistory VALUES ('20230625044933_CreateDatabase', '7.0.7');

CREATE INDEX IX_AspNetRoleClaims_RoleId ON AspNetRoleClaims (RoleId);
CREATE UNIQUE INDEX RoleNameIndex ON AspNetRoles (NormalizedName);
CREATE INDEX IX_AspNetUserClaims_UserId ON AspNetUserClaims (UserId);
CREATE INDEX IX_AspNetUserLogins_UserId ON AspNetUserLogins (UserId);
CREATE INDEX IX_AspNetUserRoles_RoleId ON AspNetUserRoles (RoleId);
CREATE INDEX EmailIndex ON AspNetUsers (NormalizedEmail);
CREATE UNIQUE INDEX UserNameIndex ON AspNetUsers (NormalizedUserName);
CREATE INDEX IX_ItemPedido_IdProduto ON ItemPedido (IdProduto);
CREATE INDEX IX_Pedido_IdCliente ON Pedido (IdCliente);
