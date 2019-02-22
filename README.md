#EcoJobScheduler
数据库自动备份服务

相关组件：
   Topshelf  用户将普通CMD程序安装为windows服务
   FluentScheduler  简单的调度组件
   
   
#使用说明

1、建议把Oracle 的备份设置在 D:\oracle\bak 目录下
2、建议把Oracle 备份服务部署在 D:\oracle\backupservice 目录下


#程序简介
 - Oracle定时备份服务.exe 是主程序

 - Config/AutoConfig.config中设置参数
~~~
<add key="ServiceGroup" value="0"/>
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
<!--数据库用户名-->
<add key="DbUser" value="demooauser"/>
<!--数据库密码-->
<add key="DbPassword" value="demooauser"/>
<!--备份目录-->
<add key="BackupDirectory" value="D:\oracle\bak"/>
<!--备份开始小时-->
<add key="StartHour" value="9"/>
<!--备份开始分钟-->
<add key="StartMinute" value="37"/>
<!--保留天数-->
<add key="Maintain" value="15"/>
</configuration>
~~~


#安装为windows服务
开启CMD，跳转到 D:\oracle\Oracle定时备份服务\ 目录下
执行 Oracle定时备份服务.exe install
>每份自动部署服务仅能支持备份一个数据库，如果要部署多个数据库需要配置多套服务，避免服务名称相同，需要修改字段【ServiceGroup】

如果需要卸载，执行 Oracle定时备份服务.exe uninstall

注意在打开CMD的是否需要使用管理员身份运行,否则无法安装为windows服务
需要关闭360等安全软件

在CMD中执行services.msc ,监察看下服务






