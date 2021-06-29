insert into [Identity].[Role](Id,Name,NormalizedName,ConcurrencyStamp)
values(NEWID(),'Creator','CREATOR',NEWID());

insert into [Identity].[Role](Id,Name,NormalizedName,ConcurrencyStamp)
values(NEWID(),'Approver','APPROVER',NEWID());

INSERT INTO [Identity].[User]
           ([Id],[UserName],[NormalizedUserName],[Email],[NormalizedEmail],[EmailConfirmed],[PasswordHash]
           ,[SecurityStamp],[ConcurrencyStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled]
           ,[LockoutEnd],[LockoutEnabled],[AccessFailedCount],[FirstName],[LastName])
VALUES (NEWID(),'Creator','CREATOR','creator@gmail.com','CREATOR@GMAIL.COM',1,'AQAAAAEAACcQAAAAEE9fE6kGsB49SPaa/w+z23trvIHHPYIcgsIHGCJ4e571nNhg7Mmnj9q5/64Y3FUKSw==',
'KFLB4TVZXNQRH67BB73ZSFLXS7HAKOMO','ab902cd6-8d33-4c47-aa6a-ae2247e0ba6e',null,0,0,null,1,0,'Creator','Creator');

INSERT INTO [Identity].[User]
           ([Id],[UserName],[NormalizedUserName],[Email],[NormalizedEmail],[EmailConfirmed],[PasswordHash]
           ,[SecurityStamp],[ConcurrencyStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled]
           ,[LockoutEnd],[LockoutEnabled],[AccessFailedCount],[FirstName],[LastName])
VALUES (NEWID(),'Approver','APPROVER','Approver@gmail.com','APPROVER@GMAIL.COM',1,'AQAAAAEAACcQAAAAEE9fE6kGsB49SPaa/w+z23trvIHHPYIcgsIHGCJ4e571nNhg7Mmnj9q5/64Y3FUKSw==',
'KFLB4TVZXNQRH67BB73ZSFLXS7HAKOMO','ab902cd6-8d33-4c47-aa6a-ae2247e0ba6e',null,0,0,null,1,0,'Approver','Approver');


INSERT INTO [Identity].[UserRoles]
select u.id UserId,r.id Roleid from [Identity].[User] u inner join [Identity].[Role] r on u.username=r.name;