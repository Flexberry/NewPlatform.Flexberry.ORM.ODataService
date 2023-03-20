



CREATE TABLE [Лес] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Название] VARCHAR(255)  NULL,

	 [Площадь] INT  NULL,

	 [Заповедник] BIT  NULL,

	 [ДатаПослОсмотра] DATETIME  NULL,

	 [Страна] UNIQUEIDENTIFIER  NULL,

	 PRIMARY KEY ([primaryKey]))

CREATE TABLE [КлассСМножТипов] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [PropertyGeography] geography  NULL,

	 [PropertyEnum] VARCHAR(6)  NULL,

	 [PropertyBool] BIT  NULL,

	 [PropertyInt] INT  NULL,

	 [PropertyDateTime] DATETIME  NULL,

	 [PropertyString] VARCHAR(255)  NULL,

	 [PropertyFloat] REAL  NULL,

	 [PropertyDouble] FLOAT  NULL,

	 [PropertyDecimal] DECIMAL  NULL,

	 [PropertySystemNullableDateTime] DATETIME  NULL,

	 [PropertySystemNullableInt] INT  NULL,

	 [PropertySystemNullableGuid] UNIQUEIDENTIFIER  NULL,

	 [PropertySystemNullableDecimal] DECIMAL  NULL,

	 [PropStormnetNullableDateTime] DATETIME  NULL,

	 [PropertyStormnetNullableInt] INT  NULL,

	 [PropertyStormnetKeyGuid] UNIQUEIDENTIFIER  NULL,

	 [PropStormnetNullableDecimal] DECIMAL  NULL,

	 [PropertyStormnetPartliedDate] VARCHAR(255)  NULL,

	 [PropertyStormnetContact] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetBlob] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetEvent] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetGeoData] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetImage] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetWebFile] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetFile] NVARCHAR(MAX)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Master] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [property] VARCHAR(255)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [LegoPatent] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Name] VARCHAR(255)  NULL,

	 [Date] DATETIME  NULL,

	 [Authors] VARCHAR(255)  NULL,

	 [Description] VARCHAR(255)  NULL,

	 [LegoBlock] UNIQUEIDENTIFIER  NULL,

	 [LegoDevice] UNIQUEIDENTIFIER  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [КлассStoredDerived] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [StrAttr2] VARCHAR(255)  NULL,

	 [StrAttr] VARCHAR(255)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Мастер] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [prop] VARCHAR(255)  NULL,

	 [Мастер2] UNIQUEIDENTIFIER  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Автор] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Имя] VARCHAR(255)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Блоха] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Кличка] VARCHAR(255)  NULL,

	 [МедведьОбитания] UNIQUEIDENTIFIER  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [КлассСоСтрокКл] (

	 [StoragePrimaryKey] VARCHAR(255)  NOT NULL,

	 PRIMARY KEY ([StoragePrimaryKey]))


CREATE TABLE [БазовыйКласс] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Свойство1] VARCHAR(255)  NULL,

	 [Свойство2] INT  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [TestDetailWithCicle] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [TestDetailName] VARCHAR(255)  NULL,

	 [Parent] UNIQUEIDENTIFIER  NULL,

	 [TestMaster] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Детейл] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [prop1] INT  NULL,

	 [БазовыйКласс_m0] UNIQUEIDENTIFIER  NULL,

	 [БазовыйКласс_m1] UNIQUEIDENTIFIER  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Driver] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Name] VARCHAR(255)  NULL,

	 [CarCount] INT  NULL,

	 [Documents] BIT  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [LegoMaterial] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Name] VARCHAR(255)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Журнал] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Название] VARCHAR(255)  NULL,

	 [Номер] INT  NULL,

	 [Автор2] UNIQUEIDENTIFIER  NOT NULL,

	 [Библиотека2] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Медведь] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [ПолеБС] VARCHAR(255)  NULL,

	 [ПорядковыйНомер] INT  NULL,

	 [Вес] INT  NULL,

	 [ЦветГлаз] VARCHAR(255)  NULL,

	 [Пол] VARCHAR(9)  NULL,

	 [ДатаРождения] DATETIME  NULL,

	 [CreateTime] DATETIME  NULL,

	 [Creator] VARCHAR(255)  NULL,

	 [EditTime] DATETIME  NULL,

	 [Editor] VARCHAR(255)  NULL,

	 [ЛесОбитания] UNIQUEIDENTIFIER  NULL,

	 [Папа] UNIQUEIDENTIFIER  NULL,

	 [Страна] UNIQUEIDENTIFIER  NULL,

	 [Мама] UNIQUEIDENTIFIER  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [TestMaster] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [TestMasterName] VARCHAR(255)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [MainClass] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Name] VARCHAR(255)  NULL,

	 [AgrClass1] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Порода] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Название] VARCHAR(255)  NULL,

	 [Иерархия_m0] UNIQUEIDENTIFIER  NULL,

	 [ТипПороды_m0] UNIQUEIDENTIFIER  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [LegoBlock] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Width] INT  NULL,

	 [Height] INT  NULL,

	 [Depth] INT  NULL,

	 [Configuration] VARCHAR(255)  NULL,

	 [Name] VARCHAR(255)  NULL,

	 [BlockId] INT  NULL,

	 [Material] UNIQUEIDENTIFIER  NULL,

	 [Color] UNIQUEIDENTIFIER  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Берлога] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [ПолеБС] VARCHAR(255)  NULL,

	 [Наименование] VARCHAR(255)  NULL,

	 [Комфортность] INT  NULL,

	 [Заброшена] BIT  NULL,

	 [Сертификат] NVARCHAR(MAX)  NULL,

	 [CertString] NVARCHAR(MAX)  NULL,

	 [ЛесРасположения] UNIQUEIDENTIFIER  NULL,

	 [ДляКакойПороды] UNIQUEIDENTIFIER  NULL,

	 [Медведь] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [LegoBlockColor] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Name] VARCHAR(255)  NULL,

	 [ColorNumber] INT  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [ПоставщикКниг] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Ссылка] UNIQUEIDENTIFIER  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Котенок] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [КличкаКотенка] VARCHAR(255)  NULL,

	 [Глупость] INT  NULL,

	 [Кошка_m0] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Daughter] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [DressColor] VARCHAR(255)  NULL,

	 [Name] VARCHAR(255)  NULL,

	 [Parent] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [LegoBlockTopPanelHole] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Position] VARCHAR(255)  NULL,

	 [TopPanel] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [DetailsClass1] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [DetailCl1Name] VARCHAR(255)  NULL,

	 [DetailsClass2] UNIQUEIDENTIFIER  NOT NULL,

	 [AgrClass1] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Car] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Number] VARCHAR(255)  NULL,

	 [Model] VARCHAR(255)  NULL,

	 [TipCar] VARCHAR(9)  NULL,

	 [driver] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [LegoBlockCustomPanel] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Orientation] VARCHAR(255)  NULL,

	 [Position] VARCHAR(255)  NULL,

	 [PanelAngle] UNIQUEIDENTIFIER  NULL,

	 [Block] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [ТипЛапы] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Название] VARCHAR(255)  NULL,

	 [Актуально] BIT  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [AgrClass1] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [AgrCl1Name] VARCHAR(255)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [LegoPanelAngle] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Name] VARCHAR(255)  NULL,

	 [Angle] INT  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Мастер2] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [свойство2] INT  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [LegoSocketStandard] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Name] VARCHAR(255)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Страна] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Название] VARCHAR(255)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Детейл2] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [prop2] VARCHAR(255)  NULL,

	 [Детейл_m0] UNIQUEIDENTIFIER  NULL,

	 [Детейл_m1] UNIQUEIDENTIFIER  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Person] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Name] VARCHAR(255)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Son] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [SuspendersColor] VARCHAR(255)  NULL,

	 [Name] VARCHAR(255)  NULL,

	 [Parent] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [AgrClass2] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [AgrCl2Name] VARCHAR(255)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [ТипПороды] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Название] VARCHAR(255)  NULL,

	 [ДатаРегистрации] DATETIME  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [LegoBlockTopPanel] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [WidthCount] INT  NULL,

	 [HeightCount] INT  NULL,

	 [SocketStandard] UNIQUEIDENTIFIER  NULL,

	 [Block] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [ДетейлНаследник] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [prop3] VARCHAR(255)  NULL,

	 [prop1] INT  NULL,

	 [БазовыйКласс_m0] UNIQUEIDENTIFIER  NULL,

	 [БазовыйКласс_m1] UNIQUEIDENTIFIER  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Библиотека] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Адрес] VARCHAR(255)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [DetailsClass2] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [DetailCl2Name] VARCHAR(255)  NULL,

	 [AgrClass2] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Книга] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Название] VARCHAR(255)  NULL,

	 [Автор1] UNIQUEIDENTIFIER  NOT NULL,

	 [Библиотека1] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [LegoDevice] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Description] VARCHAR(255)  NULL,

	 [Electricity] BIT  NULL,

	 [Name] VARCHAR(255)  NULL,

	 [BlockId] INT  NULL,

	 [Color] UNIQUEIDENTIFIER  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Наследник] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Свойство] FLOAT  NULL,

	 [Свойство1] VARCHAR(255)  NULL,

	 [Свойство2] INT  NULL,

	 [Master] UNIQUEIDENTIFIER  NULL,

	 [Мастер] UNIQUEIDENTIFIER  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [LegoBlockBottomPanel] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [WidthCount] INT  NULL,

	 [HeightCount] INT  NULL,

	 [Block] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [ДочернийКласс] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [ChildProperty] VARCHAR(255)  NULL,

	 [PropertyGeography] geography  NULL,

	 [PropertyEnum] VARCHAR(6)  NULL,

	 [PropertyBool] BIT  NULL,

	 [PropertyInt] INT  NULL,

	 [PropertyDateTime] DATETIME  NULL,

	 [PropertyString] VARCHAR(255)  NULL,

	 [PropertyFloat] REAL  NULL,

	 [PropertyDouble] FLOAT  NULL,

	 [PropertyDecimal] DECIMAL  NULL,

	 [PropertySystemNullableDateTime] DATETIME  NULL,

	 [PropertySystemNullableInt] INT  NULL,

	 [PropertySystemNullableGuid] UNIQUEIDENTIFIER  NULL,

	 [PropertySystemNullableDecimal] DECIMAL  NULL,

	 [PropStormnetNullableDateTime] DATETIME  NULL,

	 [PropertyStormnetNullableInt] INT  NULL,

	 [PropertyStormnetKeyGuid] UNIQUEIDENTIFIER  NULL,

	 [PropStormnetNullableDecimal] DECIMAL  NULL,

	 [PropertyStormnetPartliedDate] VARCHAR(255)  NULL,

	 [PropertyStormnetContact] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetBlob] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetEvent] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetGeoData] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetImage] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetWebFile] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetFile] NVARCHAR(MAX)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Перелом] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Дата] DATETIME  NULL,

	 [Тип] VARCHAR(8)  NULL,

	 [Лапа_m0] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))

CREATE TABLE [Лапа] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Цвет] VARCHAR(255)  NULL,

	 [Размер] INT  NULL,

	 [ДатаРождения] DATETIME  NULL,

	 [БылиЛиПереломы] BIT  NULL,

	 [Сторона] VARCHAR(11)  NULL,

	 [Номер] INT  NULL,

	 [РазмерDouble] FLOAT  NULL,

	 [РазмерFloat] REAL  NULL,

	 [РазмерDecimal] DECIMAL  NULL,

	 [ТипЛапы_m0] UNIQUEIDENTIFIER  NULL,

	 [Кошка_m0] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Кошка] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Кличка] VARCHAR(255)  NULL,

	 [ДатаРождения] DATETIME  NULL,

	 [Тип] VARCHAR(11)  NULL,

	 [ПородаСтрокой] VARCHAR(255)  NULL,

	 [Агрессивная] BIT  NULL,

	 [УсыСлева] INT  NULL,

	 [УсыСправа] INT  NULL,

	 [Порода_m0] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [STORMNETLOCKDATA] (

	 [LockKey] VARCHAR(300)  NOT NULL,

	 [UserName] VARCHAR(300)  NOT NULL,

	 [LockDate] DATETIME  NULL,

	 PRIMARY KEY ([LockKey]))


CREATE TABLE [STORMSETTINGS] (

	 [primaryKey] uniqueidentifier  NOT NULL,

	 [Module] varchar(1000)  NULL,

	 [Name] varchar(255)  NULL,

	 [Value] text  NULL,

	 [User] varchar(255)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [STORMAdvLimit] (

	 [primaryKey] uniqueidentifier  NOT NULL,

	 [User] varchar(255)  NULL,

	 [Published] bit  NULL,

	 [Module] varchar(255)  NULL,

	 [Name] varchar(255)  NULL,

	 [Value] text  NULL,

	 [HotKeyData] int  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [STORMFILTERSETTING] (

	 [primaryKey] uniqueidentifier  NOT NULL,

	 [Name] varchar(255)  NOT NULL,

	 [DataObjectView] varchar(255)  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [STORMWEBSEARCH] (

	 [primaryKey] uniqueidentifier  NOT NULL,

	 [Name] varchar(255)  NOT NULL,

	 [Order] INT  NOT NULL,

	 [PresentView] varchar(255)  NOT NULL,

	 [DetailedView] varchar(255)  NOT NULL,

	 [FilterSetting_m0] uniqueidentifier  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [STORMFILTERDETAIL] (

	 [primaryKey] uniqueidentifier  NOT NULL,

	 [Caption] varchar(255)  NOT NULL,

	 [DataObjectView] varchar(255)  NOT NULL,

	 [ConnectMasterProp] varchar(255)  NOT NULL,

	 [OwnerConnectProp] varchar(255)  NULL,

	 [FilterSetting_m0] uniqueidentifier  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [STORMFILTERLOOKUP] (

	 [primaryKey] uniqueidentifier  NOT NULL,

	 [DataObjectType] varchar(255)  NOT NULL,

	 [Container] varchar(255)  NULL,

	 [ContainerTag] varchar(255)  NULL,

	 [FieldsToView] varchar(255)  NULL,

	 [FilterSetting_m0] uniqueidentifier  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [UserSetting] (

	 [primaryKey] uniqueidentifier  NOT NULL,

	 [AppName] varchar(256)  NULL,

	 [UserName] varchar(512)  NULL,

	 [UserGuid] uniqueidentifier  NULL,

	 [ModuleName] varchar(1024)  NULL,

	 [ModuleGuid] uniqueidentifier  NULL,

	 [SettName] varchar(256)  NULL,

	 [SettGuid] uniqueidentifier  NULL,

	 [SettLastAccessTime] DATETIME  NULL,

	 [StrVal] varchar(256)  NULL,

	 [TxtVal] varchar(max)  NULL,

	 [IntVal] int  NULL,

	 [BoolVal] bit  NULL,

	 [GuidVal] uniqueidentifier  NULL,

	 [DecimalVal] decimal(20,10)  NULL,

	 [DateTimeVal] DATETIME  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [ApplicationLog] (

	 [primaryKey] uniqueidentifier  NOT NULL,

	 [Category] varchar(64)  NULL,

	 [EventId] INT  NULL,

	 [Priority] INT  NULL,

	 [Severity] varchar(32)  NULL,

	 [Title] varchar(256)  NULL,

	 [Timestamp] DATETIME  NULL,

	 [MachineName] varchar(32)  NULL,

	 [AppDomainName] varchar(512)  NULL,

	 [ProcessId] varchar(256)  NULL,

	 [ProcessName] varchar(512)  NULL,

	 [ThreadName] varchar(512)  NULL,

	 [Win32ThreadId] varchar(128)  NULL,

	 [Message] varchar(2500)  NULL,

	 [FormattedMessage] varchar(max)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [STORMAG] (

	 [primaryKey] uniqueidentifier  NOT NULL,

	 [Name] varchar(80)  NOT NULL,

	 [Login] varchar(50)  NULL,

	 [Pwd] varchar(50)  NULL,

	 [IsUser] bit  NOT NULL,

	 [IsGroup] bit  NOT NULL,

	 [IsRole] bit  NOT NULL,

	 [ConnString] varchar(255)  NULL,

	 [Enabled] bit  NULL,

	 [Email] varchar(80)  NULL,

	 [Comment] varchar(MAX)  NULL,

	 [CreateTime] datetime  NULL,

	 [Creator] varchar(255)  NULL,

	 [EditTime] datetime  NULL,

	 [Editor] varchar(255)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [STORMLG] (

	 [primaryKey] uniqueidentifier  NOT NULL,

	 [Group_m0] uniqueidentifier  NOT NULL,

	 [User_m0] uniqueidentifier  NOT NULL,

	 [CreateTime] datetime  NULL,

	 [Creator] varchar(255)  NULL,

	 [EditTime] datetime  NULL,

	 [Editor] varchar(255)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [STORMAuObjType] (

	 [primaryKey] uniqueidentifier  NOT NULL,

	 [Name] nvarchar(255)  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [STORMAuEntity] (

	 [primaryKey] uniqueidentifier  NOT NULL,

	 [ObjectPrimaryKey] nvarchar(38)  NOT NULL,

	 [OperationTime] DATETIME  NOT NULL,

	 [OperationType] nvarchar(100)  NOT NULL,

	 [ExecutionResult] nvarchar(12)  NOT NULL,

	 [Source] nvarchar(255)  NOT NULL,

	 [SerializedField] nvarchar(max)  NULL,

	 [User_m0] uniqueidentifier  NOT NULL,

	 [ObjectType_m0] uniqueidentifier  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [STORMAuField] (

	 [primaryKey] uniqueidentifier  NOT NULL,

	 [Field] nvarchar(100)  NOT NULL,

	 [OldValue] nvarchar(max)  NULL,

	 [NewValue] nvarchar(max)  NULL,

	 [MainChange_m0] uniqueidentifier  NULL,

	 [AuditEntity_m0] uniqueidentifier  NOT NULL,

	 PRIMARY KEY ([primaryKey]))

 ALTER TABLE [Лес] ADD CONSTRAINT [Лес_FСтрана_0] FOREIGN KEY ([Страна]) REFERENCES [Страна]
CREATE INDEX Лес_IСтрана on [Лес] ([Страна])

 ALTER TABLE [LegoPatent] ADD CONSTRAINT [LegoPatent_FLegoBlock_0] FOREIGN KEY ([LegoBlock]) REFERENCES [LegoBlock]
CREATE INDEX LegoPatent_ILegoBlock on [LegoPatent] ([LegoBlock])

 ALTER TABLE [LegoPatent] ADD CONSTRAINT [LegoPatent_FLegoDevice_0] FOREIGN KEY ([LegoDevice]) REFERENCES [LegoDevice]
CREATE INDEX LegoPatent_ILegoDevice on [LegoPatent] ([LegoDevice])

 ALTER TABLE [Мастер] ADD CONSTRAINT [Мастер_FМастер2_0] FOREIGN KEY ([Мастер2]) REFERENCES [Мастер2]
CREATE INDEX Мастер_IМастер2 on [Мастер] ([Мастер2])

 ALTER TABLE [Блоха] ADD CONSTRAINT [Блоха_FМедведь_0] FOREIGN KEY ([МедведьОбитания]) REFERENCES [Медведь]
CREATE INDEX Блоха_IМедведьОбитания on [Блоха] ([МедведьОбитания])

 ALTER TABLE [TestDetailWithCicle] ADD CONSTRAINT [TestDetailWithCicle_FTestDetailWithCicle_0] FOREIGN KEY ([Parent]) REFERENCES [TestDetailWithCicle]
CREATE INDEX TestDetailWithCicle_IParent on [TestDetailWithCicle] ([Parent])

 ALTER TABLE [TestDetailWithCicle] ADD CONSTRAINT [TestDetailWithCicle_FTestMaster_0] FOREIGN KEY ([TestMaster]) REFERENCES [TestMaster]
CREATE INDEX TestDetailWithCicle_ITestMaster on [TestDetailWithCicle] ([TestMaster])

 ALTER TABLE [Детейл] ADD CONSTRAINT [Детейл_FБазовыйКласс_0] FOREIGN KEY ([БазовыйКласс_m0]) REFERENCES [БазовыйКласс]
CREATE INDEX Детейл_IБазовыйКласс_m0 on [Детейл] ([БазовыйКласс_m0])

 ALTER TABLE [Детейл] ADD CONSTRAINT [Детейл_FНаследник_0] FOREIGN KEY ([БазовыйКласс_m1]) REFERENCES [Наследник]
CREATE INDEX Детейл_IБазовыйКласс_m1 on [Детейл] ([БазовыйКласс_m1])

 ALTER TABLE [Журнал] ADD CONSTRAINT [Журнал_FАвтор_0] FOREIGN KEY ([Автор2]) REFERENCES [Автор]
CREATE INDEX Журнал_IАвтор2 on [Журнал] ([Автор2])

 ALTER TABLE [Журнал] ADD CONSTRAINT [Журнал_FБиблиотека_0] FOREIGN KEY ([Библиотека2]) REFERENCES [Библиотека]
CREATE INDEX Журнал_IБиблиотека2 on [Журнал] ([Библиотека2])

 ALTER TABLE [Медведь] ADD CONSTRAINT [Медведь_FЛес_0] FOREIGN KEY ([ЛесОбитания]) REFERENCES [Лес]
CREATE INDEX Медведь_IЛесОбитания on [Медведь] ([ЛесОбитания])

 ALTER TABLE [Медведь] ADD CONSTRAINT [Медведь_FМедведь_0] FOREIGN KEY ([Папа]) REFERENCES [Медведь]
CREATE INDEX Медведь_IПапа on [Медведь] ([Папа])

 ALTER TABLE [Медведь] ADD CONSTRAINT [Медведь_FСтрана_0] FOREIGN KEY ([Страна]) REFERENCES [Страна]
CREATE INDEX Медведь_IСтрана on [Медведь] ([Страна])

 ALTER TABLE [Медведь] ADD CONSTRAINT [Медведь_FМедведь_1] FOREIGN KEY ([Мама]) REFERENCES [Медведь]
CREATE INDEX Медведь_IМама on [Медведь] ([Мама])

 ALTER TABLE [MainClass] ADD CONSTRAINT [MainClass_FAgrClass1_0] FOREIGN KEY ([AgrClass1]) REFERENCES [AgrClass1]
CREATE INDEX MainClass_IAgrClass1 on [MainClass] ([AgrClass1])

 ALTER TABLE [Порода] ADD CONSTRAINT [Порода_FПорода_0] FOREIGN KEY ([Иерархия_m0]) REFERENCES [Порода]
CREATE INDEX Порода_IИерархия_m0 on [Порода] ([Иерархия_m0])

 ALTER TABLE [Порода] ADD CONSTRAINT [Порода_FТипПороды_0] FOREIGN KEY ([ТипПороды_m0]) REFERENCES [ТипПороды]
CREATE INDEX Порода_IТипПороды_m0 on [Порода] ([ТипПороды_m0])

 ALTER TABLE [LegoBlock] ADD CONSTRAINT [LegoBlock_FLegoMaterial_0] FOREIGN KEY ([Material]) REFERENCES [LegoMaterial]
CREATE INDEX LegoBlock_IMaterial on [LegoBlock] ([Material])

 ALTER TABLE [LegoBlock] ADD CONSTRAINT [LegoBlock_FLegoBlockColor_0] FOREIGN KEY ([Color]) REFERENCES [LegoBlockColor]
CREATE INDEX LegoBlock_IColor on [LegoBlock] ([Color])

 ALTER TABLE [Берлога] ADD CONSTRAINT [Берлога_FЛес_0] FOREIGN KEY ([ЛесРасположения]) REFERENCES [Лес]
CREATE INDEX Берлога_IЛесРасположения on [Берлога] ([ЛесРасположения])

 ALTER TABLE [Берлога] ADD CONSTRAINT [Берлога_FПорода_0] FOREIGN KEY ([ДляКакойПороды]) REFERENCES [Порода]
CREATE INDEX Берлога_IДляКакойПороды on [Берлога] ([ДляКакойПороды])

 ALTER TABLE [Берлога] ADD CONSTRAINT [Берлога_FМедведь_0] FOREIGN KEY ([Медведь]) REFERENCES [Медведь]
CREATE INDEX Берлога_IМедведь on [Берлога] ([Медведь])

 ALTER TABLE [Котенок] ADD CONSTRAINT [Котенок_FКошка_0] FOREIGN KEY ([Кошка_m0]) REFERENCES [Кошка]
CREATE INDEX Котенок_IКошка_m0 on [Котенок] ([Кошка_m0])

 ALTER TABLE [Daughter] ADD CONSTRAINT [Daughter_FPerson_0] FOREIGN KEY ([Parent]) REFERENCES [Person]
CREATE INDEX Daughter_IParent on [Daughter] ([Parent])

 ALTER TABLE [LegoBlockTopPanelHole] ADD CONSTRAINT [LegoBlockTopPanelHole_FLegoBlockTopPanel_0] FOREIGN KEY ([TopPanel]) REFERENCES [LegoBlockTopPanel]
CREATE INDEX LegoBlockTopPanelHole_ITopPanel on [LegoBlockTopPanelHole] ([TopPanel])

 ALTER TABLE [DetailsClass1] ADD CONSTRAINT [DetailsClass1_FDetailsClass2_0] FOREIGN KEY ([DetailsClass2]) REFERENCES [DetailsClass2]
CREATE INDEX DetailsClass1_IDetailsClass2 on [DetailsClass1] ([DetailsClass2])

 ALTER TABLE [DetailsClass1] ADD CONSTRAINT [DetailsClass1_FAgrClass1_0] FOREIGN KEY ([AgrClass1]) REFERENCES [AgrClass1]
CREATE INDEX DetailsClass1_IAgrClass1 on [DetailsClass1] ([AgrClass1])

 ALTER TABLE [Car] ADD CONSTRAINT [Car_FDriver_0] FOREIGN KEY ([driver]) REFERENCES [Driver]
CREATE INDEX Car_Idriver on [Car] ([driver])

 ALTER TABLE [LegoBlockCustomPanel] ADD CONSTRAINT [LegoBlockCustomPanel_FLegoPanelAngle_0] FOREIGN KEY ([PanelAngle]) REFERENCES [LegoPanelAngle]
CREATE INDEX LegoBlockCustomPanel_IPanelAngle on [LegoBlockCustomPanel] ([PanelAngle])

 ALTER TABLE [LegoBlockCustomPanel] ADD CONSTRAINT [LegoBlockCustomPanel_FLegoBlock_0] FOREIGN KEY ([Block]) REFERENCES [LegoBlock]
CREATE INDEX LegoBlockCustomPanel_IBlock on [LegoBlockCustomPanel] ([Block])

 ALTER TABLE [Детейл2] ADD CONSTRAINT [Детейл2_FДетейл_0] FOREIGN KEY ([Детейл_m0]) REFERENCES [Детейл]
CREATE INDEX Детейл2_IДетейл_m0 on [Детейл2] ([Детейл_m0])

 ALTER TABLE [Детейл2] ADD CONSTRAINT [Детейл2_FДетейлНаследник_0] FOREIGN KEY ([Детейл_m1]) REFERENCES [ДетейлНаследник]
CREATE INDEX Детейл2_IДетейл_m1 on [Детейл2] ([Детейл_m1])

 ALTER TABLE [Son] ADD CONSTRAINT [Son_FPerson_0] FOREIGN KEY ([Parent]) REFERENCES [Person]
CREATE INDEX Son_IParent on [Son] ([Parent])

 ALTER TABLE [LegoBlockTopPanel] ADD CONSTRAINT [LegoBlockTopPanel_FLegoSocketStandard_0] FOREIGN KEY ([SocketStandard]) REFERENCES [LegoSocketStandard]
CREATE INDEX LegoBlockTopPanel_ISocketStandard on [LegoBlockTopPanel] ([SocketStandard])

 ALTER TABLE [LegoBlockTopPanel] ADD CONSTRAINT [LegoBlockTopPanel_FLegoBlock_0] FOREIGN KEY ([Block]) REFERENCES [LegoBlock]
CREATE INDEX LegoBlockTopPanel_IBlock on [LegoBlockTopPanel] ([Block])

 ALTER TABLE [ДетейлНаследник] ADD CONSTRAINT [ДетейлНаследник_FБазовыйКласс_0] FOREIGN KEY ([БазовыйКласс_m0]) REFERENCES [БазовыйКласс]
CREATE INDEX ДетейлНаследник_IБазовыйКласс_m0 on [ДетейлНаследник] ([БазовыйКласс_m0])

 ALTER TABLE [ДетейлНаследник] ADD CONSTRAINT [ДетейлНаследник_FНаследник_0] FOREIGN KEY ([БазовыйКласс_m1]) REFERENCES [Наследник]
CREATE INDEX ДетейлНаследник_IБазовыйКласс_m1 on [ДетейлНаследник] ([БазовыйКласс_m1])

 ALTER TABLE [DetailsClass2] ADD CONSTRAINT [DetailsClass2_FAgrClass2_0] FOREIGN KEY ([AgrClass2]) REFERENCES [AgrClass2]
CREATE INDEX DetailsClass2_IAgrClass2 on [DetailsClass2] ([AgrClass2])

 ALTER TABLE [Книга] ADD CONSTRAINT [Книга_FАвтор_0] FOREIGN KEY ([Автор1]) REFERENCES [Автор]
CREATE INDEX Книга_IАвтор1 on [Книга] ([Автор1])

 ALTER TABLE [Книга] ADD CONSTRAINT [Книга_FБиблиотека_0] FOREIGN KEY ([Библиотека1]) REFERENCES [Библиотека]
CREATE INDEX Книга_IБиблиотека1 on [Книга] ([Библиотека1])

 ALTER TABLE [LegoDevice] ADD CONSTRAINT [LegoDevice_FLegoBlockColor_0] FOREIGN KEY ([Color]) REFERENCES [LegoBlockColor]
CREATE INDEX LegoDevice_IColor on [LegoDevice] ([Color])

 ALTER TABLE [Наследник] ADD CONSTRAINT [Наследник_FMaster_0] FOREIGN KEY ([Master]) REFERENCES [Master]
CREATE INDEX Наследник_IMaster on [Наследник] ([Master])

 ALTER TABLE [Наследник] ADD CONSTRAINT [Наследник_FМастер_0] FOREIGN KEY ([Мастер]) REFERENCES [Мастер]
CREATE INDEX Наследник_IМастер on [Наследник] ([Мастер])

 ALTER TABLE [LegoBlockBottomPanel] ADD CONSTRAINT [LegoBlockBottomPanel_FLegoBlock_0] FOREIGN KEY ([Block]) REFERENCES [LegoBlock]
CREATE INDEX LegoBlockBottomPanel_IBlock on [LegoBlockBottomPanel] ([Block])

 ALTER TABLE [Перелом] ADD CONSTRAINT [Перелом_FЛапа_0] FOREIGN KEY ([Лапа_m0]) REFERENCES [Лапа]
CREATE INDEX Перелом_IЛапа_m0 on [Перелом] ([Лапа_m0])

 ALTER TABLE [Лапа] ADD CONSTRAINT [Лапа_FТипЛапы_0] FOREIGN KEY ([ТипЛапы_m0]) REFERENCES [ТипЛапы]
CREATE INDEX Лапа_IТипЛапы_m0 on [Лапа] ([ТипЛапы_m0])

 ALTER TABLE [Лапа] ADD CONSTRAINT [Лапа_FКошка_0] FOREIGN KEY ([Кошка_m0]) REFERENCES [Кошка]
CREATE INDEX Лапа_IКошка_m0 on [Лапа] ([Кошка_m0])

 ALTER TABLE [Кошка] ADD CONSTRAINT [Кошка_FПорода_0] FOREIGN KEY ([Порода_m0]) REFERENCES [Порода]
CREATE INDEX Кошка_IПорода_m0 on [Кошка] ([Порода_m0])

 ALTER TABLE [STORMWEBSEARCH] ADD CONSTRAINT [STORMWEBSEARCH_FSTORMFILTERSETTING_0] FOREIGN KEY ([FilterSetting_m0]) REFERENCES [STORMFILTERSETTING]

 ALTER TABLE [STORMFILTERDETAIL] ADD CONSTRAINT [STORMFILTERDETAIL_FSTORMFILTERSETTING_0] FOREIGN KEY ([FilterSetting_m0]) REFERENCES [STORMFILTERSETTING]

 ALTER TABLE [STORMFILTERLOOKUP] ADD CONSTRAINT [STORMFILTERLOOKUP_FSTORMFILTERSETTING_0] FOREIGN KEY ([FilterSetting_m0]) REFERENCES [STORMFILTERSETTING]

 ALTER TABLE [STORMLG] ADD CONSTRAINT [STORMLG_FSTORMAG_0] FOREIGN KEY ([Group_m0]) REFERENCES [STORMAG]

 ALTER TABLE [STORMLG] ADD CONSTRAINT [STORMLG_FSTORMAG_1] FOREIGN KEY ([User_m0]) REFERENCES [STORMAG]

 ALTER TABLE [STORMAuEntity] ADD CONSTRAINT [STORMAuEntity_FSTORMAG_0] FOREIGN KEY ([User_m0]) REFERENCES [STORMAG]

 ALTER TABLE [STORMAuEntity] ADD CONSTRAINT [STORMAuEntity_FSTORMAuObjType_0] FOREIGN KEY ([ObjectType_m0]) REFERENCES [STORMAuObjType]

 ALTER TABLE [STORMAuField] ADD CONSTRAINT [STORMAuField_FSTORMAuField_0] FOREIGN KEY ([MainChange_m0]) REFERENCES [STORMAuField]

 ALTER TABLE [STORMAuField] ADD CONSTRAINT [STORMAuField_FSTORMAuEntity_0] FOREIGN KEY ([AuditEntity_m0]) REFERENCES [STORMAuEntity]

