# Rental Management System

## Group Member
|Name|Matric no.|
|:---:|:---:|
|KELVIN EE|A20EC0195|
|CHLOE RACQUELMAE KENNEDY|A20EC0026|
|HONG PEI GEOK|A20EC0044|
|SINGTHAI SRISOI|A20EC0147|
|ONG HAN WAH|A20EC0129|

## How to run
1. Download project folder + database (by visual studio code or command prompt: git clone https://github.com/peiyu00/AD.git)
2. Import database
![image](https://github.com/MQiLEE/AD/assets/95162273/8c6a25f3-b5bc-4b3e-abab-688f65176249)

3. Open web.config file and change the connection string according to your SQL server name. 
```
  <connectionStrings>
	  <add name="db_XyTechEntitiesMQ" connectionString="metadata=res://*/Models.Model1.csdl|res://*/Models.Model1.ssdl|res://*/Models.Model1.msl;provider=System.Data.SqlClient;provider connection string=&quot;data source=LAPTOP-VAAJ4EQ4\SQLEXPRESS;initial catalog=db_XyTech;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework&quot;" providerName="System.Data.EntityClient" />
  </connectionStrings>
```
change this
```
data source=LAPTOP-VAAJ4EQ4\SQLEXPRESS
```
