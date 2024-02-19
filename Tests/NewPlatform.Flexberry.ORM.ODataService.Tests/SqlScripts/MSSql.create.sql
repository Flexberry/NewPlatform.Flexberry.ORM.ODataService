



CREATE TABLE [КлассСМножТипов] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [PropertyBool] BIT  NULL,

	 [PropertyDateTime] DATETIME  NULL,

	 [PropertyDecimal] DECIMAL  NULL,

	 [PropertyDouble] FLOAT  NULL,

	 [PropertyEnum] VARCHAR(6)  NULL,

	 [PropertyFloat] REAL  NULL,

	 [PropertyGeography] geography  NULL,

	 [PropertyInt] INT  NULL,

	 [PropertyStormnetBlob] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetContact] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetEvent] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetFile] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetGeoData] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetImage] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetKeyGuid] UNIQUEIDENTIFIER  NULL,

	 [PropStormnetNullableDateTime] DATETIME  NULL,

	 [PropStormnetNullableDecimal] DECIMAL  NULL,

	 [PropertyStormnetNullableInt] INT  NULL,

	 [PropertyStormnetPartliedDate] VARCHAR(255)  NULL,

	 [PropertyStormnetWebFile] NVARCHAR(MAX)  NULL,

	 [PropertyString] VARCHAR(255)  NULL,

	 [PropertySystemNullableDateTime] DATETIME  NULL,

	 [PropertySystemNullableDecimal] DECIMAL  NULL,

	 [PropertySystemNullableGuid] UNIQUEIDENTIFIER  NULL,

	 [PropertySystemNullableInt] INT  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Master] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [property] VARCHAR(255)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Лес] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [ДатаПослОсмотра] DATETIME  NULL,

	 [Заповедник] BIT  NULL,

	 [Название] VARCHAR(255)  NULL,

	 [Площадь] INT  NULL,

	 [Страна] UNIQUEIDENTIFIER  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [LegoPatent] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Authors] VARCHAR(255)  NULL,

	 [Date] DATETIME  NULL,

	 [Description] VARCHAR(255)  NULL,

	 [Name] VARCHAR(255)  NULL,

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

	 [CarCount] INT  NULL,

	 [Documents] BIT  NULL,

	 [Name] VARCHAR(255)  NULL,

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

	 [Configuration] VARCHAR(255)  NULL,

	 [Depth] INT  NULL,

	 [Height] INT  NULL,

	 [Width] INT  NULL,

	 [BlockId] INT  NULL,

	 [Name] VARCHAR(255)  NULL,

	 [Material] UNIQUEIDENTIFIER  NULL,

	 [Color] UNIQUEIDENTIFIER  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [LegoBlockColor] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [ColorNumber] INT  NULL,

	 [Name] VARCHAR(255)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [ПоставщикКниг] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Ссылка] UNIQUEIDENTIFIER  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Котенок] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Глупость] INT  NULL,

	 [КличкаКотенка] VARCHAR(255)  NULL,

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


CREATE TABLE [LegoBlockCustomPanel] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Orientation] VARCHAR(255)  NULL,

	 [Position] VARCHAR(255)  NULL,

	 [PanelAngle] UNIQUEIDENTIFIER  NULL,

	 [Block] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [ТипЛапы] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Актуально] BIT  NULL,

	 [Название] VARCHAR(255)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [AgrClass1] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [AgrCl1Name] VARCHAR(255)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [LegoPanelAngle] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Angle] INT  NULL,

	 [Name] VARCHAR(255)  NULL,

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

	 [ДатаРегистрации] DATETIME  NULL,

	 [Название] VARCHAR(255)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [LegoBlockTopPanel] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [HeightCount] INT  NULL,

	 [WidthCount] INT  NULL,

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

	 [BlockId] INT  NULL,

	 [Name] VARCHAR(255)  NULL,

	 [Color] UNIQUEIDENTIFIER  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Наследник] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Свойство] FLOAT  NULL,

	 [Свойство1] VARCHAR(255)  NULL,

	 [Свойство2] INT  NULL,

	 [Мастер] UNIQUEIDENTIFIER  NULL,

	 [Master] UNIQUEIDENTIFIER  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [LegoBlockBottomPanel] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [HeightCount] INT  NULL,

	 [WidthCount] INT  NULL,

	 [Block] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [ДочернийКласс] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [ChildProperty] VARCHAR(255)  NULL,

	 [PropertyBool] BIT  NULL,

	 [PropertyDateTime] DATETIME  NULL,

	 [PropertyDecimal] DECIMAL  NULL,

	 [PropertyDouble] FLOAT  NULL,

	 [PropertyEnum] VARCHAR(6)  NULL,

	 [PropertyFloat] REAL  NULL,

	 [PropertyGeography] geography  NULL,

	 [PropertyInt] INT  NULL,

	 [PropertyStormnetBlob] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetContact] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetEvent] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetFile] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetGeoData] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetImage] NVARCHAR(MAX)  NULL,

	 [PropertyStormnetKeyGuid] UNIQUEIDENTIFIER  NULL,

	 [PropStormnetNullableDateTime] DATETIME  NULL,

	 [PropStormnetNullableDecimal] DECIMAL  NULL,

	 [PropertyStormnetNullableInt] INT  NULL,

	 [PropertyStormnetPartliedDate] VARCHAR(255)  NULL,

	 [PropertyStormnetWebFile] NVARCHAR(MAX)  NULL,

	 [PropertyString] VARCHAR(255)  NULL,

	 [PropertySystemNullableDateTime] DATETIME  NULL,

	 [PropertySystemNullableDecimal] DECIMAL  NULL,

	 [PropertySystemNullableGuid] UNIQUEIDENTIFIER  NULL,

	 [PropertySystemNullableInt] INT  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Медведь] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Вес] INT  NULL,

	 [ДатаРождения] DATETIME  NULL,

	 [Пол] VARCHAR(9)  NULL,

	 [ПолеБС] VARCHAR(255)  NULL,

	 [ПорядковыйНомер] INT  NULL,

	 [ЦветГлаз] VARCHAR(255)  NULL,

	 [CreateTime] DATETIME  NULL,

	 [Creator] VARCHAR(255)  NULL,

	 [Editor] VARCHAR(255)  NULL,

	 [EditTime] DATETIME  NULL,

	 [ЛесОбитания] UNIQUEIDENTIFIER  NULL,

	 [Мама] UNIQUEIDENTIFIER  NULL,

	 [Папа] UNIQUEIDENTIFIER  NULL,

	 [Страна] UNIQUEIDENTIFIER  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Берлога] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Заброшена] BIT  NULL,

	 [Комфортность] INT  NULL,

	 [Наименование] VARCHAR(255)  NULL,

	 [ПолеБС] VARCHAR(255)  NULL,

	 [Сертификат] NVARCHAR(MAX)  NULL,

	 [CertString] NVARCHAR(MAX)  NULL,

	 [ЛесРасположения] UNIQUEIDENTIFIER  NULL,

	 [ДляКакойПороды] UNIQUEIDENTIFIER  NULL,

	 [Медведь] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Car] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Model] VARCHAR(255)  NULL,

	 [Number] VARCHAR(255)  NULL,

	 [TipCar] VARCHAR(9)  NULL,

	 [driver] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Перелом] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Дата] DATETIME  NULL,

	 [Тип] VARCHAR(8)  NULL,

	 [Лапа_m0] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Лапа] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [БылиЛиПереломы] BIT  NULL,

	 [ДатаРождения] DATETIME  NULL,

	 [Номер] INT  NULL,

	 [Размер] INT  NULL,

	 [РазмерDecimal] DECIMAL  NULL,

	 [РазмерDouble] FLOAT  NULL,

	 [РазмерFloat] REAL  NULL,

	 [Сторона] VARCHAR(11)  NULL,

	 [Цвет] VARCHAR(255)  NULL,

	 [ТипЛапы_m0] UNIQUEIDENTIFIER  NULL,

	 [Кошка_m0] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [Кошка] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Агрессивная] BIT  NULL,

	 [ДатаРождения] DATETIME  NULL,

	 [Кличка] VARCHAR(255)  NULL,

	 [УсыСлева] INT  NULL,

	 [УсыСправа] INT  NULL,

	 [ПородаСтрокой] VARCHAR(255)  NULL,

	 [Тип] VARCHAR(11)  NULL,

	 [Порода_m0] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [DetailAndMaster] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Name] VARCHAR(255)  NULL,

	 [Agregator] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [TestConfiguration] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Name] VARCHAR(255)  NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [TestClass] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Name] VARCHAR(255)  NULL,

	 [FirstLevel] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [ThirdLevel] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Name] VARCHAR(255)  NULL,

	 [TestClass] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [SecondLevel1] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Name] VARCHAR(255)  NULL,

	 [FirstLevel] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [FirstLevel] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Name] VARCHAR(255)  NULL,

	 [TestConfiguration] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [SecondLevel2] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Name] VARCHAR(255)  NULL,

	 [SecondLevel1_m0] UNIQUEIDENTIFIER  NULL,

	 [SecondLevel1_m1] UNIQUEIDENTIFIER  NULL,

	 [FirstLevel] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [TestAssociation] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Name2] VARCHAR(255)  NULL,

	 [Name] VARCHAR(255)  NULL,

	 [SecondLevel1_m0] UNIQUEIDENTIFIER  NULL,

	 [SecondLevel1_m1] UNIQUEIDENTIFIER  NULL,

	 [FirstLevel] UNIQUEIDENTIFIER  NOT NULL,

	 PRIMARY KEY ([primaryKey]))


CREATE TABLE [AgregatorSameMD] (

	 [primaryKey] UNIQUEIDENTIFIER  NOT NULL,

	 [Name] VARCHAR(255)  NULL,

	 [Master] UNIQUEIDENTIFIER  NULL,

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




