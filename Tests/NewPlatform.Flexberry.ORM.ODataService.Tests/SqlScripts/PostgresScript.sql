




CREATE TABLE LegoPanelAngle (

 primaryKey UUID NOT NULL,

 Name VARCHAR(255) NULL,

 Angle INT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Driver (

 primaryKey UUID NOT NULL,

 Name VARCHAR(255) NULL,

 CarCount INT NULL,

 Documents BOOLEAN NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Страна (

 primaryKey UUID NOT NULL,

 Название VARCHAR(255) NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Master (

 primaryKey UUID NOT NULL,

 property VARCHAR(255) NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Лес (

 primaryKey UUID NOT NULL,

 Название VARCHAR(255) NULL,

 Площадь INT NULL,

 Заповедник BOOLEAN NULL,

 ДатаПослОсмотра TIMESTAMP(3) NULL,

 Страна UUID NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Son (

 primaryKey UUID NOT NULL,

 SuspendersColor VARCHAR(255) NULL,

 Name VARCHAR(255) NULL,

 Parent UUID NOT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE ТипПороды (

 primaryKey UUID NOT NULL,

 Название VARCHAR(255) NULL,

 ДатаРегистрации TIMESTAMP(3) NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Порода (

 primaryKey UUID NOT NULL,

 Название VARCHAR(255) NULL,

 ТипПороды_m0 UUID NULL,

 Иерархия_m0 UUID NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE TestDetailWithCicle (

 primaryKey UUID NOT NULL,

 TestDetailName VARCHAR(255) NULL,

 Parent UUID NULL,

 TestMaster UUID NOT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE LegoBlockCustomPanel (

 primaryKey UUID NOT NULL,

 Orientation VARCHAR(255) NULL,

 Position VARCHAR(255) NULL,

 PanelAngle UUID NULL,

 Block UUID NOT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Daughter (

 primaryKey UUID NOT NULL,

 DressColor VARCHAR(255) NULL,

 Name VARCHAR(255) NULL,

 Parent UUID NOT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE КлассСМножТипов (

 primaryKey UUID NOT NULL,

 PropertyGeography GEOMETRY NULL,

 PropertyEnum VARCHAR(6) NULL,

 PropertyBool BOOLEAN NULL,

 PropertyInt INT NULL,

 PropertyDateTime TIMESTAMP(3) NULL,

 PropertyString VARCHAR(255) NULL,

 PropertyFloat REAL NULL,

 PropertyDouble DOUBLE PRECISION NULL,

 PropertyDecimal DECIMAL NULL,

 PropertySystemNullableDateTime TIMESTAMP(3) NULL,

 PropertySystemNullableInt INT NULL,

 PropertySystemNullableGuid UUID NULL,

 PropertySystemNullableDecimal DECIMAL NULL,

 PropStormnetNullableDateTime TIMESTAMP(3) NULL,

 PropertyStormnetNullableInt INT NULL,

 PropertyStormnetKeyGuid UUID NULL,

 PropStormnetNullableDecimal DECIMAL NULL,

 PropertyStormnetPartliedDate VARCHAR(255) NULL,

 PropertyStormnetContact TEXT NULL,

 PropertyStormnetBlob TEXT NULL,

 PropertyStormnetEvent TEXT NULL,

 PropertyStormnetGeoData TEXT NULL,

 PropertyStormnetImage TEXT NULL,

 PropertyStormnetWebFile TEXT NULL,

 PropertyStormnetFile TEXT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Медведь (

 primaryKey UUID NOT NULL,

 ПолеБС VARCHAR(255) NULL,

 ПорядковыйНомер INT NULL,

 Вес INT NULL,

 ЦветГлаз VARCHAR(255) NULL,

 Пол VARCHAR(9) NULL,

 ДатаРождения TIMESTAMP(3) NULL,

 CreateTime TIMESTAMP(3) NULL,

 Creator VARCHAR(255) NULL,

 EditTime TIMESTAMP(3) NULL,

 Editor VARCHAR(255) NULL,

 Страна UUID NULL,

 ЛесОбитания UUID NULL,

 Мама UUID NULL,

 Папа UUID NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE LegoBlockColor (

 primaryKey UUID NOT NULL,

 Name VARCHAR(255) NULL,

 ColorNumber INT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Детейл (

 primaryKey UUID NOT NULL,

 prop1 INT NULL,

 БазовыйКласс_m0 UUID NULL,

 БазовыйКласс_m1 UUID NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE ДочернийКласс (

 primaryKey UUID NOT NULL,

 ChildProperty VARCHAR(255) NULL,

 PropertyGeography GEOMETRY NULL,

 PropertyEnum VARCHAR(6) NULL,

 PropertyBool BOOLEAN NULL,

 PropertyInt INT NULL,

 PropertyDateTime TIMESTAMP(3) NULL,

 PropertyString VARCHAR(255) NULL,

 PropertyFloat REAL NULL,

 PropertyDouble DOUBLE PRECISION NULL,

 PropertyDecimal DECIMAL NULL,

 PropertySystemNullableDateTime TIMESTAMP(3) NULL,

 PropertySystemNullableInt INT NULL,

 PropertySystemNullableGuid UUID NULL,

 PropertySystemNullableDecimal DECIMAL NULL,

 PropStormnetNullableDateTime TIMESTAMP(3) NULL,

 PropertyStormnetNullableInt INT NULL,

 PropertyStormnetKeyGuid UUID NULL,

 PropStormnetNullableDecimal DECIMAL NULL,

 PropertyStormnetPartliedDate VARCHAR(255) NULL,

 PropertyStormnetContact TEXT NULL,

 PropertyStormnetBlob TEXT NULL,

 PropertyStormnetEvent TEXT NULL,

 PropertyStormnetGeoData TEXT NULL,

 PropertyStormnetImage TEXT NULL,

 PropertyStormnetWebFile TEXT NULL,

 PropertyStormnetFile TEXT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Книга (

 primaryKey UUID NOT NULL,

 Название VARCHAR(255) NULL,

 Автор1 UUID NOT NULL,

 Библиотека1 UUID NOT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE LegoBlock (

 primaryKey UUID NOT NULL,

 Width INT NULL,

 Height INT NULL,

 Depth INT NULL,

 Configuration VARCHAR(255) NULL,

 Name VARCHAR(255) NULL,

 BlockId INT NULL,

 Material UUID NULL,

 Color UUID NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Person (

 primaryKey UUID NOT NULL,

 Name VARCHAR(255) NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE LegoSocketStandard (

 primaryKey UUID NOT NULL,

 Name VARCHAR(255) NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Перелом (

 primaryKey UUID NOT NULL,

 Дата TIMESTAMP(3) NULL,

 Тип VARCHAR(8) NULL,

 Лапа_m0 UUID NOT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE БазовыйКласс (

 primaryKey UUID NOT NULL,

 Свойство1 VARCHAR(255) NULL,

 Свойство2 INT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE ПоставщикКниг (

 primaryKey UUID NOT NULL,

 Ссылка UUID NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Мастер2 (

 primaryKey UUID NOT NULL,

 свойство2 INT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE LegoDevice (

 primaryKey UUID NOT NULL,

 Description VARCHAR(255) NULL,

 Electricity BOOLEAN NULL,

 Name VARCHAR(255) NULL,

 BlockId INT NULL,

 Color UUID NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Наследник (

 primaryKey UUID NOT NULL,

 Свойство DOUBLE PRECISION NULL,

 Свойство1 VARCHAR(255) NULL,

 Свойство2 INT NULL,

 Мастер UUID NULL,

 Master UUID NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Car (

 primaryKey UUID NOT NULL,

 Number VARCHAR(255) NULL,

 Model VARCHAR(255) NULL,

 TipCar VARCHAR(9) NULL,

 driver UUID NOT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Мастер (

 primaryKey UUID NOT NULL,

 prop VARCHAR(255) NULL,

 Мастер2 UUID NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Блоха (

 primaryKey UUID NOT NULL,

 Кличка VARCHAR(255) NULL,

 МедведьОбитания UUID NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE LegoPatent (

 primaryKey UUID NOT NULL,

 Name VARCHAR(255) NULL,

 Date TIMESTAMP(3) NULL,

 Authors VARCHAR(255) NULL,

 Description VARCHAR(255) NULL,

 LegoBlock UUID NULL,

 LegoDevice UUID NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Лапа (

 primaryKey UUID NOT NULL,

 Цвет VARCHAR(255) NULL,

 Размер INT NULL,

 ДатаРождения TIMESTAMP(3) NULL,

 БылиЛиПереломы BOOLEAN NULL,

 Сторона VARCHAR(11) NULL,

 Номер INT NULL,

 РазмерDouble DOUBLE PRECISION NULL,

 РазмерFloat REAL NULL,

 РазмерDecimal DECIMAL NULL,

 ТипЛапы_m0 UUID NULL,

 Кошка_m0 UUID NOT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE КлассСоСтрокКл (

 StoragePrimaryKey VARCHAR(255) NOT NULL,

 PRIMARY KEY (StoragePrimaryKey));



CREATE TABLE Берлога (

 primaryKey UUID NOT NULL,

 ПолеБС VARCHAR(255) NULL,

 Наименование VARCHAR(255) NULL,

 Комфортность INT NULL,

 Заброшена BOOLEAN NULL,

 Сертификат TEXT NULL,

 CertString TEXT NULL,

 ЛесРасположения UUID NULL,

 Медведь UUID NOT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Автор (

 primaryKey UUID NOT NULL,

 Имя VARCHAR(255) NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE TestMaster (

 primaryKey UUID NOT NULL,

 TestMasterName VARCHAR(255) NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Котенок (

 primaryKey UUID NOT NULL,

 КличкаКотенка VARCHAR(255) NULL,

 Глупость INT NULL,

 Кошка_m0 UUID NOT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Библиотека (

 primaryKey UUID NOT NULL,

 Адрес VARCHAR(255) NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE LegoMaterial (

 primaryKey UUID NOT NULL,

 Name VARCHAR(255) NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE LegoBlockBottomPanel (

 primaryKey UUID NOT NULL,

 WidthCount INT NULL,

 HeightCount INT NULL,

 Block UUID NOT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE LegoBlockTopPanelHole (

 primaryKey UUID NOT NULL,

 Position VARCHAR(255) NULL,

 TopPanel UUID NOT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Детейл2 (

 primaryKey UUID NOT NULL,

 prop2 VARCHAR(255) NULL,

 Детейл_m0 UUID NULL,

 Детейл_m1 UUID NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Журнал (

 primaryKey UUID NOT NULL,

 Название VARCHAR(255) NULL,

 Номер INT NULL,

 Автор2 UUID NOT NULL,

 Библиотека2 UUID NOT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE LegoBlockTopPanel (

 primaryKey UUID NOT NULL,

 WidthCount INT NULL,

 HeightCount INT NULL,

 SocketStandard UUID NULL,

 Block UUID NOT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE ТипЛапы (

 primaryKey UUID NOT NULL,

 Название VARCHAR(255) NULL,

 Актуально BOOLEAN NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE КлассStoredDerived (

 primaryKey UUID NOT NULL,

 StrAttr2 VARCHAR(255) NULL,

 StrAttr VARCHAR(255) NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE Кошка (

 primaryKey UUID NOT NULL,

 Кличка VARCHAR(255) NULL,

 ДатаРождения TIMESTAMP(3) NULL,

 Тип VARCHAR(11) NULL,

 ПородаСтрокой VARCHAR(255) NULL,

 Агрессивная BOOLEAN NULL,

 УсыСлева INT NULL,

 УсыСправа INT NULL,

 Порода_m0 UUID NOT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE ДетейлНаследник (

 primaryKey UUID NOT NULL,

 prop3 VARCHAR(255) NULL,

 prop1 INT NULL,

 БазовыйКласс_m0 UUID NULL,

 БазовыйКласс_m1 UUID NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE STORMNETLOCKDATA (

 LockKey VARCHAR(300) NOT NULL,

 UserName VARCHAR(300) NOT NULL,

 LockDate TIMESTAMP(3) NULL,

 PRIMARY KEY (LockKey));



CREATE TABLE STORMSETTINGS (

 primaryKey UUID NOT NULL,

 Module VARCHAR(1000) NULL,

 Name VARCHAR(255) NULL,

 Value TEXT NULL,

 "User" VARCHAR(255) NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE STORMAdvLimit (

 primaryKey UUID NOT NULL,

 "User" VARCHAR(255) NULL,

 Published BOOLEAN NULL,

 Module VARCHAR(255) NULL,

 Name VARCHAR(255) NULL,

 Value TEXT NULL,

 HotKeyData INT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE STORMFILTERSETTING (

 primaryKey UUID NOT NULL,

 Name VARCHAR(255) NOT NULL,

 DataObjectView VARCHAR(255) NOT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE STORMWEBSEARCH (

 primaryKey UUID NOT NULL,

 Name VARCHAR(255) NOT NULL,

 "Order" INT NOT NULL,

 PresentView VARCHAR(255) NOT NULL,

 DetailedView VARCHAR(255) NOT NULL,

 FilterSetting_m0 UUID NOT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE STORMFILTERDETAIL (

 primaryKey UUID NOT NULL,

 Caption VARCHAR(255) NOT NULL,

 DataObjectView VARCHAR(255) NOT NULL,

 ConnectMasterProp VARCHAR(255) NOT NULL,

 OwnerConnectProp VARCHAR(255) NULL,

 FilterSetting_m0 UUID NOT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE STORMFILTERLOOKUP (

 primaryKey UUID NOT NULL,

 DataObjectType VARCHAR(255) NOT NULL,

 Container VARCHAR(255) NULL,

 ContainerTag VARCHAR(255) NULL,

 FieldsToView VARCHAR(255) NULL,

 FilterSetting_m0 UUID NOT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE UserSetting (

 primaryKey UUID NOT NULL,

 AppName VARCHAR(256) NULL,

 UserName VARCHAR(512) NULL,

 UserGuid UUID NULL,

 ModuleName VARCHAR(1024) NULL,

 ModuleGuid UUID NULL,

 SettName VARCHAR(256) NULL,

 SettGuid UUID NULL,

 SettLastAccessTime TIMESTAMP(3) NULL,

 StrVal VARCHAR(256) NULL,

 TxtVal TEXT NULL,

 IntVal INT NULL,

 BoolVal BOOLEAN NULL,

 GuidVal UUID NULL,

 DecimalVal DECIMAL(20,10) NULL,

 DateTimeVal TIMESTAMP(3) NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE ApplicationLog (

 primaryKey UUID NOT NULL,

 Category VARCHAR(64) NULL,

 EventId INT NULL,

 Priority INT NULL,

 Severity VARCHAR(32) NULL,

 Title VARCHAR(256) NULL,

 Timestamp TIMESTAMP(3) NULL,

 MachineName VARCHAR(32) NULL,

 AppDomainName VARCHAR(512) NULL,

 ProcessId VARCHAR(256) NULL,

 ProcessName VARCHAR(512) NULL,

 ThreadName VARCHAR(512) NULL,

 Win32ThreadId VARCHAR(128) NULL,

 Message VARCHAR(2500) NULL,

 FormattedMessage TEXT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE STORMAuObjType (

 primaryKey UUID NOT NULL,

 Name VARCHAR(255) NOT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE STORMAuEntity (

 primaryKey UUID NOT NULL,

 ObjectPrimaryKey VARCHAR(38) NOT NULL,

 OperationTime TIMESTAMP(3) NOT NULL,

 OperationType VARCHAR(100) NOT NULL,

 ExecutionResult VARCHAR(12) NOT NULL,

 Source VARCHAR(255) NOT NULL,

 SerializedField TEXT NULL,

 User_m0 UUID NOT NULL,

 ObjectType_m0 UUID NOT NULL,

 PRIMARY KEY (primaryKey));



CREATE TABLE STORMAuField (

 primaryKey UUID NOT NULL,

 Field VARCHAR(100) NOT NULL,

 OldValue TEXT NULL,

 NewValue TEXT NULL,

 MainChange_m0 UUID NULL,

 AuditEntity_m0 UUID NOT NULL,

 PRIMARY KEY (primaryKey));




 ALTER TABLE Лес ADD CONSTRAINT FK157288d4eaf64bbfb67adbcb2075fa4a FOREIGN KEY (Страна) REFERENCES Страна; 
CREATE INDEX Indexd3bd1222072f531605e73e66656fe58296c8bfd2 on Лес (Страна); 

 ALTER TABLE Son ADD CONSTRAINT FK591a988367264dec8e0b539bf3a27668 FOREIGN KEY (Parent) REFERENCES Person; 
CREATE INDEX Indexdabfac99e0885f846c9da12b79c4cbd7894842d3 on Son (Parent); 

 ALTER TABLE Порода ADD CONSTRAINT FK59eaeb4d663b4848811150258de0d19d FOREIGN KEY (ТипПороды_m0) REFERENCES ТипПороды; 
CREATE INDEX Index2b114b4677a6ea52b53a26c9a20bbaad606eff78 on Порода (ТипПороды_m0); 

 ALTER TABLE Порода ADD CONSTRAINT FKe4d6f93777bc4488a33dcdb5ac5ac326 FOREIGN KEY (Иерархия_m0) REFERENCES Порода; 
CREATE INDEX Index14420958a87702decd2090afa109ad90292eb458 on Порода (Иерархия_m0); 

 ALTER TABLE TestDetailWithCicle ADD CONSTRAINT FK33618bf6577c4eb5846549a35b8cad2f FOREIGN KEY (Parent) REFERENCES TestDetailWithCicle; 
CREATE INDEX Index04dbb0ac2005483472591018ab7522c499fe5b38 on TestDetailWithCicle (Parent); 

 ALTER TABLE TestDetailWithCicle ADD CONSTRAINT FK660b9cb165a242938963ad5736da9987 FOREIGN KEY (TestMaster) REFERENCES TestMaster; 
CREATE INDEX Indexf8067bbec7353839c222df5b7aeba4ce65c1655f on TestDetailWithCicle (TestMaster); 

 ALTER TABLE LegoBlockCustomPanel ADD CONSTRAINT FKd2703633d2c2491a844ae2c217e9a581 FOREIGN KEY (PanelAngle) REFERENCES LegoPanelAngle; 
CREATE INDEX Index53e2141b982788b2970c50d23073a5847e47d81c on LegoBlockCustomPanel (PanelAngle); 

 ALTER TABLE LegoBlockCustomPanel ADD CONSTRAINT FK5732cb16bd954414a9263ad04d958141 FOREIGN KEY (Block) REFERENCES LegoBlock; 
CREATE INDEX Indexff526da3c540c5b99c203d0d93050c62ed29505f on LegoBlockCustomPanel (Block); 

 ALTER TABLE Daughter ADD CONSTRAINT FK167076390fb44b538625b92a974adb06 FOREIGN KEY (Parent) REFERENCES Person; 
CREATE INDEX Index743208308d3826e12250804dbe77e02601e27402 on Daughter (Parent); 
CREATE INDEX Indexc5f9d71c0704ea7b966afcfdc89dc7eabe2d0c43 on КлассСМножТипов USING gist (PropertyGeography); 

 ALTER TABLE Медведь ADD CONSTRAINT FK0ab12cc5d7ff447b89812fd65417b140 FOREIGN KEY (Страна) REFERENCES Страна; 
CREATE INDEX Index6d8033494746b0bb87ba367c83d273dfa11b8f59 on Медведь (Страна); 

 ALTER TABLE Медведь ADD CONSTRAINT FK79a77b908b924630bc08a5719e6b50be FOREIGN KEY (ЛесОбитания) REFERENCES Лес; 
CREATE INDEX Index93be01a32cae64dc4b18705ade6683f41a32c367 on Медведь (ЛесОбитания); 

 ALTER TABLE Медведь ADD CONSTRAINT FK8e93bdaf1c7d45dda7a08ab69efb46c2 FOREIGN KEY (Мама) REFERENCES Медведь; 
CREATE INDEX Index0b9f6ad0caded1971696ef6602e8a2831fa941b1 on Медведь (Мама); 

 ALTER TABLE Медведь ADD CONSTRAINT FK2221bdb7cbc34984864b4a62e09320a7 FOREIGN KEY (Папа) REFERENCES Медведь; 
CREATE INDEX Index0ca403a899ac5a709a19bbb9ada47b0060e5b819 on Медведь (Папа); 

 ALTER TABLE Детейл ADD CONSTRAINT FK923566cb6a084c329f7d2acacd98f174 FOREIGN KEY (БазовыйКласс_m0) REFERENCES БазовыйКласс; 
CREATE INDEX Indexffdec7cb63189ed3e206c50c005f7daa0fe24f75 on Детейл (БазовыйКласс_m0); 

 ALTER TABLE Детейл ADD CONSTRAINT FKa7336dd6f1ea480b904213a02bfabdb3 FOREIGN KEY (БазовыйКласс_m1) REFERENCES Наследник; 
CREATE INDEX Indexafd5bc7b595e3649b3c31ff899c0c8d3bd2219cc on Детейл (БазовыйКласс_m1); 
CREATE INDEX Index10d981ac5288ea278311b6a711b43fa954b635a8 on ДочернийКласс USING gist (PropertyGeography); 

 ALTER TABLE Книга ADD CONSTRAINT FKf633df113e654a6db7dbadac5e6a96da FOREIGN KEY (Автор1) REFERENCES Автор; 
CREATE INDEX Index899896abd3f04413fb054cc8507b69f51489a8bc on Книга (Автор1); 

 ALTER TABLE Книга ADD CONSTRAINT FK854a716c283040479578b54070ad4eb5 FOREIGN KEY (Библиотека1) REFERENCES Библиотека; 
CREATE INDEX Index84e4d9d5b6adc4bec48541216f15f396dd2b602c on Книга (Библиотека1); 

 ALTER TABLE LegoBlock ADD CONSTRAINT FKcd5f622d3293434daf6486250cc886de FOREIGN KEY (Material) REFERENCES LegoMaterial; 
CREATE INDEX Index4cfb6296b76f57183c4e2ee17b66be5bed463d0f on LegoBlock (Material); 

 ALTER TABLE LegoBlock ADD CONSTRAINT FKa94ea1ef9d0f4ae6a03e598afc7f50cf FOREIGN KEY (Color) REFERENCES LegoBlockColor; 
CREATE INDEX Index9f2f5e18e6674969f5e82da37bf4b5a2a1d4713a on LegoBlock (Color); 

 ALTER TABLE Перелом ADD CONSTRAINT FK5e03356919e8443ba43caec6decfb2ea FOREIGN KEY (Лапа_m0) REFERENCES Лапа; 
CREATE INDEX Index6dee404d2bb9702d8d72537c5ae42a7c97dfb5fa on Перелом (Лапа_m0); 

 ALTER TABLE LegoDevice ADD CONSTRAINT FK35ac24b8f55d4bc3b4c30a037afec198 FOREIGN KEY (Color) REFERENCES LegoBlockColor; 
CREATE INDEX Indexafee67e1a41c2550c0f3df3724edf1b0be74db9f on LegoDevice (Color); 

 ALTER TABLE Наследник ADD CONSTRAINT FK73a33a56a9db4d23b3edd7980d678f20 FOREIGN KEY (Мастер) REFERENCES Мастер; 
CREATE INDEX Index28ac1d61524a43a59e67af57a855ad487d1f8141 on Наследник (Мастер); 

 ALTER TABLE Наследник ADD CONSTRAINT FK05604bae8988472a9f6a0160d2669f2d FOREIGN KEY (Master) REFERENCES Master; 
CREATE INDEX Index0cb9ac2b0e7896223ed63c0c888c23aa86682b1e on Наследник (Master); 

 ALTER TABLE Car ADD CONSTRAINT FK8820c93ffe4b4fc79cf93f17420c2ca5 FOREIGN KEY (driver) REFERENCES Driver; 
CREATE INDEX Indexe86a1d047f4df342bf39a5af864aec2b40a3547a on Car (driver); 

 ALTER TABLE Мастер ADD CONSTRAINT FK5fbf5b67b43f475aa0af523e48ad5c52 FOREIGN KEY (Мастер2) REFERENCES Мастер2; 
CREATE INDEX Index0053148ab4597a6e8d749a7201b40246de6bba66 on Мастер (Мастер2); 

 ALTER TABLE Блоха ADD CONSTRAINT FKc8b16acd02404f5195d98cec9a2eef3b FOREIGN KEY (МедведьОбитания) REFERENCES Медведь; 
CREATE INDEX Indexb43131b348ee335105dd990a690720791b5dcba6 on Блоха (МедведьОбитания); 

 ALTER TABLE LegoPatent ADD CONSTRAINT FK9cfdab44ae544a2392df6c879604bf8c FOREIGN KEY (LegoBlock) REFERENCES LegoBlock; 
CREATE INDEX Index849bc80cb1cf2d804429cfd10a756cb914e3fece on LegoPatent (LegoBlock); 

 ALTER TABLE LegoPatent ADD CONSTRAINT FKa04432c6fd544677a1b19a648b524d8e FOREIGN KEY (LegoDevice) REFERENCES LegoDevice; 
CREATE INDEX Index935a233bb659ed7d9c097089601b6ef70a1dc6df on LegoPatent (LegoDevice); 

 ALTER TABLE Лапа ADD CONSTRAINT FKb94f432b705f4fc6991cd1aab6ad65f1 FOREIGN KEY (ТипЛапы_m0) REFERENCES ТипЛапы; 
CREATE INDEX Index801cdef07db8852f60bd68a5a1fc42341cd641fa on Лапа (ТипЛапы_m0); 

 ALTER TABLE Лапа ADD CONSTRAINT FK846dc9bf967442f5a85940594c7959f8 FOREIGN KEY (Кошка_m0) REFERENCES Кошка; 
CREATE INDEX Indexd2c2995f4deb3767b25fa4ca17d61bf9bff3d562 on Лапа (Кошка_m0); 

 ALTER TABLE Берлога ADD CONSTRAINT FKc2709eb2d6874fa89862f667540fd80f FOREIGN KEY (ЛесРасположения) REFERENCES Лес; 
CREATE INDEX Indexa74603e81cb82d318a92d5d3e374895fe242d80e on Берлога (ЛесРасположения); 

 ALTER TABLE Берлога ADD CONSTRAINT FK1f260f70167d47438f738925a8287890 FOREIGN KEY (Медведь) REFERENCES Медведь; 
CREATE INDEX Index838e30a686c4f1dcfbb02e55d47218e48ddbe7a2 on Берлога (Медведь); 

 ALTER TABLE Котенок ADD CONSTRAINT FKd61230cda1ce42dea4a12e3a51526845 FOREIGN KEY (Кошка_m0) REFERENCES Кошка; 
CREATE INDEX Index79a0f583830fea7f95d716cc96a27d0967a2d537 on Котенок (Кошка_m0); 

 ALTER TABLE LegoBlockBottomPanel ADD CONSTRAINT FK02ed28f02f144a98845a958962102cc8 FOREIGN KEY (Block) REFERENCES LegoBlock; 
CREATE INDEX Indexba0e912f138a900a9d416b459d0b38e70871f043 on LegoBlockBottomPanel (Block); 

 ALTER TABLE LegoBlockTopPanelHole ADD CONSTRAINT FKf768898b17b743d9a63fd2d04c736963 FOREIGN KEY (TopPanel) REFERENCES LegoBlockTopPanel; 
CREATE INDEX Indexdeb8814715bfe1e7ac2e15c7346e05312d5a6a61 on LegoBlockTopPanelHole (TopPanel); 

 ALTER TABLE Детейл2 ADD CONSTRAINT FK022a6378aa5c4b27acbd11616bd04476 FOREIGN KEY (Детейл_m0) REFERENCES Детейл; 
CREATE INDEX Indexf07848a075b735870c7821349481da65acdab1ac on Детейл2 (Детейл_m0); 

 ALTER TABLE Детейл2 ADD CONSTRAINT FKd6a50786d5fa43c497b5f58d4c1dccd8 FOREIGN KEY (Детейл_m1) REFERENCES ДетейлНаследник; 
CREATE INDEX Indexcf374070a402ac74a423ee1902b2348b95969951 on Детейл2 (Детейл_m1); 

 ALTER TABLE Журнал ADD CONSTRAINT FKffb609bc1203460ea083045acc6cf87d FOREIGN KEY (Автор2) REFERENCES Автор; 
CREATE INDEX Index51fce8ede8f7716be28b6975505b1f9e738cc71c on Журнал (Автор2); 

 ALTER TABLE Журнал ADD CONSTRAINT FK9a0b356d87d74eea901c75f734c97f0c FOREIGN KEY (Библиотека2) REFERENCES Библиотека; 
CREATE INDEX Index93c8f6bdc15c74cea64c3d56754d8263c4f0ceb5 on Журнал (Библиотека2); 

 ALTER TABLE LegoBlockTopPanel ADD CONSTRAINT FK211491cf9d8d4da698bffe2230235117 FOREIGN KEY (SocketStandard) REFERENCES LegoSocketStandard; 
CREATE INDEX Index49d972c58446da0e86dfc1da2b8f54a4df9a37d0 on LegoBlockTopPanel (SocketStandard); 

 ALTER TABLE LegoBlockTopPanel ADD CONSTRAINT FKfdf663e9075e405fb494ce62236fd845 FOREIGN KEY (Block) REFERENCES LegoBlock; 
CREATE INDEX Indexa1f8021b49abe4367a71a7f42c5e578c3326580a on LegoBlockTopPanel (Block); 

 ALTER TABLE Кошка ADD CONSTRAINT FK96c7b526967c40de9b46446b72d55a0d FOREIGN KEY (Порода_m0) REFERENCES Порода; 
CREATE INDEX Index271599c8f6730bbff77fe5e9bf61dbfd89e661c6 on Кошка (Порода_m0); 

 ALTER TABLE ДетейлНаследник ADD CONSTRAINT FK043edf55e74f4884b658db2c29cc20e9 FOREIGN KEY (БазовыйКласс_m0) REFERENCES БазовыйКласс; 
CREATE INDEX Index5f71c50187d44a2fbbee65273ce3d50bdf1e0311 on ДетейлНаследник (БазовыйКласс_m0); 

 ALTER TABLE ДетейлНаследник ADD CONSTRAINT FK75dc70e3100348278829b09878717143 FOREIGN KEY (БазовыйКласс_m1) REFERENCES Наследник; 
CREATE INDEX Index4c265a3e468425e25ea55c8344c1110b7cb4bde6 on ДетейлНаследник (БазовыйКласс_m1); 

 ALTER TABLE STORMWEBSEARCH ADD CONSTRAINT FKf2814f935401433ab00725ed790be71f FOREIGN KEY (FilterSetting_m0) REFERENCES STORMFILTERSETTING; 

 ALTER TABLE STORMFILTERDETAIL ADD CONSTRAINT FKc1850f8ae71b43c18065af8b3899bf8a FOREIGN KEY (FilterSetting_m0) REFERENCES STORMFILTERSETTING; 

 ALTER TABLE STORMFILTERLOOKUP ADD CONSTRAINT FK658f79f07f904608aa98b03582c313c5 FOREIGN KEY (FilterSetting_m0) REFERENCES STORMFILTERSETTING; 

 ALTER TABLE STORMAuEntity ADD CONSTRAINT FK6ea8300d794e42758d1cb3ee166aa092 FOREIGN KEY (ObjectType_m0) REFERENCES STORMAuObjType; 

 ALTER TABLE STORMAuField ADD CONSTRAINT FK122112fb319548a9930ff994de6e17e2 FOREIGN KEY (MainChange_m0) REFERENCES STORMAuField; 

 ALTER TABLE STORMAuField ADD CONSTRAINT FK43ce00da26154e06954d4ddcddf5aa0e FOREIGN KEY (AuditEntity_m0) REFERENCES STORMAuEntity; 

