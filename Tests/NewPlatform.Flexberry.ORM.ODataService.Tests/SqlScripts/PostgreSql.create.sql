




CREATE TABLE LegoPanelAngle (
 primaryKey UUID NOT NULL,
 Name VARCHAR(255) NULL,
 Angle INT NULL,
 PRIMARY KEY (primaryKey));


CREATE TABLE MainClass (
 primaryKey UUID NOT NULL,
 Name VARCHAR(255) NULL,
 AgrClass1 UUID NOT NULL,
 PRIMARY KEY (primaryKey));


CREATE TABLE Driver (
 primaryKey UUID NOT NULL,
 Name VARCHAR(255) NULL,
 CarCount INT NULL,
 Documents BOOLEAN NULL,
 PRIMARY KEY (primaryKey));


CREATE TABLE DetailsClass1 (
 primaryKey UUID NOT NULL,
 DetailCl1Name VARCHAR(255) NULL,
 DetailsClass2 UUID NOT NULL,
 AgrClass1 UUID NOT NULL,
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


CREATE TABLE AgrClass1 (
 primaryKey UUID NOT NULL,
 AgrCl1Name VARCHAR(255) NULL,
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


CREATE TABLE DetailsClass2 (
 primaryKey UUID NOT NULL,
 DetailCl2Name VARCHAR(255) NULL,
 AgrClass2 UUID NOT NULL,
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


CREATE TABLE AgrClass2 (
 primaryKey UUID NOT NULL,
 AgrCl2Name VARCHAR(255) NULL,
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



 ALTER TABLE MainClass ADD CONSTRAINT FK685ab69befdffb9de6fa545fb70cd50905f15ac0 FOREIGN KEY (AgrClass1) REFERENCES AgrClass1; 
CREATE INDEX Index685ab69befdffb9de6fa545fb70cd50905f15ac0 on MainClass (AgrClass1); 

 ALTER TABLE DetailsClass1 ADD CONSTRAINT FKfb36ebac9beeb237cccb40b7be9cf2d25e9247b9 FOREIGN KEY (DetailsClass2) REFERENCES DetailsClass2; 
CREATE INDEX Indexfb36ebac9beeb237cccb40b7be9cf2d25e9247b9 on DetailsClass1 (DetailsClass2); 

 ALTER TABLE DetailsClass1 ADD CONSTRAINT FK10455ebe27fd31555ad2502ede3706a815784321 FOREIGN KEY (AgrClass1) REFERENCES AgrClass1; 
CREATE INDEX Index10455ebe27fd31555ad2502ede3706a815784321 on DetailsClass1 (AgrClass1); 

 ALTER TABLE Лес ADD CONSTRAINT FKd3bd1222072f531605e73e66656fe58296c8bfd2 FOREIGN KEY (Страна) REFERENCES Страна; 
CREATE INDEX Indexd3bd1222072f531605e73e66656fe58296c8bfd2 on Лес (Страна); 

 ALTER TABLE Son ADD CONSTRAINT FKdabfac99e0885f846c9da12b79c4cbd7894842d3 FOREIGN KEY (Parent) REFERENCES Person; 
CREATE INDEX Indexdabfac99e0885f846c9da12b79c4cbd7894842d3 on Son (Parent); 

 ALTER TABLE Порода ADD CONSTRAINT FK2b114b4677a6ea52b53a26c9a20bbaad606eff78 FOREIGN KEY (ТипПороды_m0) REFERENCES ТипПороды; 
CREATE INDEX Index2b114b4677a6ea52b53a26c9a20bbaad606eff78 on Порода (ТипПороды_m0); 

 ALTER TABLE Порода ADD CONSTRAINT FK14420958a87702decd2090afa109ad90292eb458 FOREIGN KEY (Иерархия_m0) REFERENCES Порода; 
CREATE INDEX Index14420958a87702decd2090afa109ad90292eb458 on Порода (Иерархия_m0); 

 ALTER TABLE TestDetailWithCicle ADD CONSTRAINT FK04dbb0ac2005483472591018ab7522c499fe5b38 FOREIGN KEY (Parent) REFERENCES TestDetailWithCicle; 
CREATE INDEX Index04dbb0ac2005483472591018ab7522c499fe5b38 on TestDetailWithCicle (Parent); 

 ALTER TABLE TestDetailWithCicle ADD CONSTRAINT FKf8067bbec7353839c222df5b7aeba4ce65c1655f FOREIGN KEY (TestMaster) REFERENCES TestMaster; 
CREATE INDEX Indexf8067bbec7353839c222df5b7aeba4ce65c1655f on TestDetailWithCicle (TestMaster); 

 ALTER TABLE LegoBlockCustomPanel ADD CONSTRAINT FK53e2141b982788b2970c50d23073a5847e47d81c FOREIGN KEY (PanelAngle) REFERENCES LegoPanelAngle; 
CREATE INDEX Index53e2141b982788b2970c50d23073a5847e47d81c on LegoBlockCustomPanel (PanelAngle); 

 ALTER TABLE LegoBlockCustomPanel ADD CONSTRAINT FKff526da3c540c5b99c203d0d93050c62ed29505f FOREIGN KEY (Block) REFERENCES LegoBlock; 
CREATE INDEX Indexff526da3c540c5b99c203d0d93050c62ed29505f on LegoBlockCustomPanel (Block); 

 ALTER TABLE Daughter ADD CONSTRAINT FK743208308d3826e12250804dbe77e02601e27402 FOREIGN KEY (Parent) REFERENCES Person; 
CREATE INDEX Index743208308d3826e12250804dbe77e02601e27402 on Daughter (Parent); 
CREATE INDEX Indexc5f9d71c0704ea7b966afcfdc89dc7eabe2d0c43 on КлассСМножТипов USING gist (PropertyGeography); 

 ALTER TABLE Медведь ADD CONSTRAINT FK6d8033494746b0bb87ba367c83d273dfa11b8f59 FOREIGN KEY (Страна) REFERENCES Страна; 
CREATE INDEX Index6d8033494746b0bb87ba367c83d273dfa11b8f59 on Медведь (Страна); 

 ALTER TABLE Медведь ADD CONSTRAINT FK93be01a32cae64dc4b18705ade6683f41a32c367 FOREIGN KEY (ЛесОбитания) REFERENCES Лес; 
CREATE INDEX Index93be01a32cae64dc4b18705ade6683f41a32c367 on Медведь (ЛесОбитания); 

 ALTER TABLE Медведь ADD CONSTRAINT FK0b9f6ad0caded1971696ef6602e8a2831fa941b1 FOREIGN KEY (Мама) REFERENCES Медведь; 
CREATE INDEX Index0b9f6ad0caded1971696ef6602e8a2831fa941b1 on Медведь (Мама); 

 ALTER TABLE Медведь ADD CONSTRAINT FK0ca403a899ac5a709a19bbb9ada47b0060e5b819 FOREIGN KEY (Папа) REFERENCES Медведь; 
CREATE INDEX Index0ca403a899ac5a709a19bbb9ada47b0060e5b819 on Медведь (Папа); 

 ALTER TABLE Детейл ADD CONSTRAINT FKffdec7cb63189ed3e206c50c005f7daa0fe24f75 FOREIGN KEY (БазовыйКласс_m0) REFERENCES БазовыйКласс; 
CREATE INDEX Indexffdec7cb63189ed3e206c50c005f7daa0fe24f75 on Детейл (БазовыйКласс_m0); 

 ALTER TABLE Детейл ADD CONSTRAINT FKafd5bc7b595e3649b3c31ff899c0c8d3bd2219cc FOREIGN KEY (БазовыйКласс_m1) REFERENCES Наследник; 
CREATE INDEX Indexafd5bc7b595e3649b3c31ff899c0c8d3bd2219cc on Детейл (БазовыйКласс_m1); 
CREATE INDEX Index10d981ac5288ea278311b6a711b43fa954b635a8 on ДочернийКласс USING gist (PropertyGeography); 

 ALTER TABLE Книга ADD CONSTRAINT FK899896abd3f04413fb054cc8507b69f51489a8bc FOREIGN KEY (Автор1) REFERENCES Автор; 
CREATE INDEX Index899896abd3f04413fb054cc8507b69f51489a8bc on Книга (Автор1); 

 ALTER TABLE Книга ADD CONSTRAINT FK84e4d9d5b6adc4bec48541216f15f396dd2b602c FOREIGN KEY (Библиотека1) REFERENCES Библиотека; 
CREATE INDEX Index84e4d9d5b6adc4bec48541216f15f396dd2b602c on Книга (Библиотека1); 

 ALTER TABLE LegoBlock ADD CONSTRAINT FK4cfb6296b76f57183c4e2ee17b66be5bed463d0f FOREIGN KEY (Material) REFERENCES LegoMaterial; 
CREATE INDEX Index4cfb6296b76f57183c4e2ee17b66be5bed463d0f on LegoBlock (Material); 

 ALTER TABLE LegoBlock ADD CONSTRAINT FK9f2f5e18e6674969f5e82da37bf4b5a2a1d4713a FOREIGN KEY (Color) REFERENCES LegoBlockColor; 
CREATE INDEX Index9f2f5e18e6674969f5e82da37bf4b5a2a1d4713a on LegoBlock (Color); 

 ALTER TABLE Перелом ADD CONSTRAINT FK6dee404d2bb9702d8d72537c5ae42a7c97dfb5fa FOREIGN KEY (Лапа_m0) REFERENCES Лапа; 
CREATE INDEX Index6dee404d2bb9702d8d72537c5ae42a7c97dfb5fa on Перелом (Лапа_m0); 

 ALTER TABLE LegoDevice ADD CONSTRAINT FKafee67e1a41c2550c0f3df3724edf1b0be74db9f FOREIGN KEY (Color) REFERENCES LegoBlockColor; 
CREATE INDEX Indexafee67e1a41c2550c0f3df3724edf1b0be74db9f on LegoDevice (Color); 

 ALTER TABLE Наследник ADD CONSTRAINT FK28ac1d61524a43a59e67af57a855ad487d1f8141 FOREIGN KEY (Мастер) REFERENCES Мастер; 
CREATE INDEX Index28ac1d61524a43a59e67af57a855ad487d1f8141 on Наследник (Мастер); 

 ALTER TABLE Наследник ADD CONSTRAINT FK0cb9ac2b0e7896223ed63c0c888c23aa86682b1e FOREIGN KEY (Master) REFERENCES Master; 
CREATE INDEX Index0cb9ac2b0e7896223ed63c0c888c23aa86682b1e on Наследник (Master); 

 ALTER TABLE DetailsClass2 ADD CONSTRAINT FK5dd594070037c791874553ac01ea6fb3502b7a4f FOREIGN KEY (AgrClass2) REFERENCES AgrClass2; 
CREATE INDEX Index5dd594070037c791874553ac01ea6fb3502b7a4f on DetailsClass2 (AgrClass2); 

 ALTER TABLE Car ADD CONSTRAINT FKe86a1d047f4df342bf39a5af864aec2b40a3547a FOREIGN KEY (driver) REFERENCES Driver; 
CREATE INDEX Indexe86a1d047f4df342bf39a5af864aec2b40a3547a on Car (driver); 

 ALTER TABLE Мастер ADD CONSTRAINT FK0053148ab4597a6e8d749a7201b40246de6bba66 FOREIGN KEY (Мастер2) REFERENCES Мастер2; 
CREATE INDEX Index0053148ab4597a6e8d749a7201b40246de6bba66 on Мастер (Мастер2); 

 ALTER TABLE Блоха ADD CONSTRAINT FKb43131b348ee335105dd990a690720791b5dcba6 FOREIGN KEY (МедведьОбитания) REFERENCES Медведь; 
CREATE INDEX Indexb43131b348ee335105dd990a690720791b5dcba6 on Блоха (МедведьОбитания); 

 ALTER TABLE LegoPatent ADD CONSTRAINT FK849bc80cb1cf2d804429cfd10a756cb914e3fece FOREIGN KEY (LegoBlock) REFERENCES LegoBlock; 
CREATE INDEX Index849bc80cb1cf2d804429cfd10a756cb914e3fece on LegoPatent (LegoBlock); 

 ALTER TABLE LegoPatent ADD CONSTRAINT FK935a233bb659ed7d9c097089601b6ef70a1dc6df FOREIGN KEY (LegoDevice) REFERENCES LegoDevice; 
CREATE INDEX Index935a233bb659ed7d9c097089601b6ef70a1dc6df on LegoPatent (LegoDevice); 

 ALTER TABLE Лапа ADD CONSTRAINT FK801cdef07db8852f60bd68a5a1fc42341cd641fa FOREIGN KEY (ТипЛапы_m0) REFERENCES ТипЛапы; 
CREATE INDEX Index801cdef07db8852f60bd68a5a1fc42341cd641fa on Лапа (ТипЛапы_m0); 

 ALTER TABLE Лапа ADD CONSTRAINT FKd2c2995f4deb3767b25fa4ca17d61bf9bff3d562 FOREIGN KEY (Кошка_m0) REFERENCES Кошка; 
CREATE INDEX Indexd2c2995f4deb3767b25fa4ca17d61bf9bff3d562 on Лапа (Кошка_m0); 

 ALTER TABLE Берлога ADD CONSTRAINT FKa74603e81cb82d318a92d5d3e374895fe242d80e FOREIGN KEY (ЛесРасположения) REFERENCES Лес; 
CREATE INDEX Indexa74603e81cb82d318a92d5d3e374895fe242d80e on Берлога (ЛесРасположения); 

 ALTER TABLE Берлога ADD CONSTRAINT FK838e30a686c4f1dcfbb02e55d47218e48ddbe7a2 FOREIGN KEY (Медведь) REFERENCES Медведь; 
CREATE INDEX Index838e30a686c4f1dcfbb02e55d47218e48ddbe7a2 on Берлога (Медведь); 

 ALTER TABLE Котенок ADD CONSTRAINT FK79a0f583830fea7f95d716cc96a27d0967a2d537 FOREIGN KEY (Кошка_m0) REFERENCES Кошка; 
CREATE INDEX Index79a0f583830fea7f95d716cc96a27d0967a2d537 on Котенок (Кошка_m0); 

 ALTER TABLE LegoBlockBottomPanel ADD CONSTRAINT FKba0e912f138a900a9d416b459d0b38e70871f043 FOREIGN KEY (Block) REFERENCES LegoBlock; 
CREATE INDEX Indexba0e912f138a900a9d416b459d0b38e70871f043 on LegoBlockBottomPanel (Block); 

 ALTER TABLE LegoBlockTopPanelHole ADD CONSTRAINT FKdeb8814715bfe1e7ac2e15c7346e05312d5a6a61 FOREIGN KEY (TopPanel) REFERENCES LegoBlockTopPanel; 
CREATE INDEX Indexdeb8814715bfe1e7ac2e15c7346e05312d5a6a61 on LegoBlockTopPanelHole (TopPanel); 

 ALTER TABLE Детейл2 ADD CONSTRAINT FKf07848a075b735870c7821349481da65acdab1ac FOREIGN KEY (Детейл_m0) REFERENCES Детейл; 
CREATE INDEX Indexf07848a075b735870c7821349481da65acdab1ac on Детейл2 (Детейл_m0); 

 ALTER TABLE Детейл2 ADD CONSTRAINT FKcf374070a402ac74a423ee1902b2348b95969951 FOREIGN KEY (Детейл_m1) REFERENCES ДетейлНаследник; 
CREATE INDEX Indexcf374070a402ac74a423ee1902b2348b95969951 on Детейл2 (Детейл_m1); 

 ALTER TABLE Журнал ADD CONSTRAINT FK51fce8ede8f7716be28b6975505b1f9e738cc71c FOREIGN KEY (Автор2) REFERENCES Автор; 
CREATE INDEX Index51fce8ede8f7716be28b6975505b1f9e738cc71c on Журнал (Автор2); 

 ALTER TABLE Журнал ADD CONSTRAINT FK93c8f6bdc15c74cea64c3d56754d8263c4f0ceb5 FOREIGN KEY (Библиотека2) REFERENCES Библиотека; 
CREATE INDEX Index93c8f6bdc15c74cea64c3d56754d8263c4f0ceb5 on Журнал (Библиотека2); 

 ALTER TABLE LegoBlockTopPanel ADD CONSTRAINT FK49d972c58446da0e86dfc1da2b8f54a4df9a37d0 FOREIGN KEY (SocketStandard) REFERENCES LegoSocketStandard; 
CREATE INDEX Index49d972c58446da0e86dfc1da2b8f54a4df9a37d0 on LegoBlockTopPanel (SocketStandard); 

 ALTER TABLE LegoBlockTopPanel ADD CONSTRAINT FKa1f8021b49abe4367a71a7f42c5e578c3326580a FOREIGN KEY (Block) REFERENCES LegoBlock; 
CREATE INDEX Indexa1f8021b49abe4367a71a7f42c5e578c3326580a on LegoBlockTopPanel (Block); 

 ALTER TABLE Кошка ADD CONSTRAINT FK271599c8f6730bbff77fe5e9bf61dbfd89e661c6 FOREIGN KEY (Порода_m0) REFERENCES Порода; 
CREATE INDEX Index271599c8f6730bbff77fe5e9bf61dbfd89e661c6 on Кошка (Порода_m0); 

 ALTER TABLE ДетейлНаследник ADD CONSTRAINT FK5f71c50187d44a2fbbee65273ce3d50bdf1e0311 FOREIGN KEY (БазовыйКласс_m0) REFERENCES БазовыйКласс; 
CREATE INDEX Index5f71c50187d44a2fbbee65273ce3d50bdf1e0311 on ДетейлНаследник (БазовыйКласс_m0); 

 ALTER TABLE ДетейлНаследник ADD CONSTRAINT FK4c265a3e468425e25ea55c8344c1110b7cb4bde6 FOREIGN KEY (БазовыйКласс_m1) REFERENCES Наследник; 
CREATE INDEX Index4c265a3e468425e25ea55c8344c1110b7cb4bde6 on ДетейлНаследник (БазовыйКласс_m1); 

 ALTER TABLE STORMWEBSEARCH ADD CONSTRAINT FKc4378e39870eb056aec84088683297a01d2a6200 FOREIGN KEY (FilterSetting_m0) REFERENCES STORMFILTERSETTING; 

 ALTER TABLE STORMFILTERDETAIL ADD CONSTRAINT FK921d16269835017e2a0d0e29ad6fb175454a70d0 FOREIGN KEY (FilterSetting_m0) REFERENCES STORMFILTERSETTING; 

 ALTER TABLE STORMFILTERLOOKUP ADD CONSTRAINT FKce38ef0db3f01a53acaa49fed8853fb941ad47ba FOREIGN KEY (FilterSetting_m0) REFERENCES STORMFILTERSETTING; 
CREATE INDEX Indexaa1a3fec50499d204c389473163d0d8f55d4aed9 on STORMAuEntity (ObjectPrimaryKey); 
CREATE INDEX Indexa06334f170abdcbe9ebbf9a1c97a105e31caac8d on STORMAuEntity (upper(ObjectPrimaryKey)); 
CREATE INDEX Index969964c4b120bd7eebed319d77e182a5adf20816 on STORMAuEntity (OperationTime); 
CREATE INDEX Index44feded66c1cee358e0db313bcaa06e5f8d8e549 on STORMAuEntity (User_m0); 

 ALTER TABLE STORMAuEntity ADD CONSTRAINT FKb5725f55e665c6b660aff02c558b5ba413523eaa FOREIGN KEY (ObjectType_m0) REFERENCES STORMAuObjType; 
CREATE INDEX Indexb5725f55e665c6b660aff02c558b5ba413523eaa on STORMAuEntity (ObjectType_m0); 

 ALTER TABLE STORMAuField ADD CONSTRAINT FKf2cc121c707b1bf4290f2bb625d1d112b4919288 FOREIGN KEY (MainChange_m0) REFERENCES STORMAuField; 

 ALTER TABLE STORMAuField ADD CONSTRAINT FK680dbb7e20a2404a7439d4de2d06d669f165bafe FOREIGN KEY (AuditEntity_m0) REFERENCES STORMAuEntity; 
CREATE INDEX Index680dbb7e20a2404a7439d4de2d06d669f165bafe on STORMAuField (AuditEntity_m0); 

