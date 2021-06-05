CREATE TABLE [dbo].[Clothes] (
    [Id]                  INT             NOT NULL IDENTITY,
    [Shirt_name]          VARCHAR (20)    NULL,
    [Shirt_model]         VARBINARY (MAX) NULL,
    [Shirt_material_name] VARCHAR (20)    NULL,
    [Shirt_material]      VARBINARY (MAX) NULL,
    [Shirt_Design]        VARBINARY (MAX) NULL,
    [Pant_name]           VARCHAR (20)    NULL,
    [Pant_model]          VARBINARY (MAX) NULL,
    [Pant_material_name]  VARCHAR (20)    NULL,
    [Pant_material]       VARBINARY (MAX) NULL,
    [Pant_design]         VARBINARY (MAX) NULL, 
    CONSTRAINT [PK_Clothes] PRIMARY KEY ([Id])
);

