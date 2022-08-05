USE [StudentRecord]
GO

/****** Object: Table [dbo].[AcademicRecord] Script Date: 9/17/2017 10:42:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
DROP TABLE [dbo].[AcademicRecord];
GO
DROP TABLE [dbo].[Registration];
GO
DROP TABLE [dbo].[Course];
GO
DROP TABLE [dbo].[Student];
GO
DROP TABLE [dbo].[Employee_Role];
GO
DROP TABLE [dbo].[Employee];
GO
DROP TABLE [dbo].[Role];
GO

CREATE TABLE [dbo].[Course] (
    [Code]  NVARCHAR (16) NOT NULL PRIMARY KEY,
    [Title] NVARCHAR (50) NOT NULL,
    [Description]  VARCHAR (MAX)  NULL,
    [HoursPerWeek] INT            NULL,
    [FeeBase]      DECIMAL (6, 2) NULL,
);

CREATE TABLE [dbo].[Student] (
    [Id]   NVARCHAR (16) NOT NULL PRIMARY KEY,
    [Name]       VARCHAR (50) NOT NULL,
);

CREATE TABLE [dbo].[AcademicRecord]
(
	[CourseCode] NVARCHAR(16) NOT NULL , 
    [StudentId] NVARCHAR(16) NOT NULL, 
    [Grade] INT NULL, 
    PRIMARY KEY ([StudentId], [CourseCode]), 
    CONSTRAINT [FK_AcademicRecord_Course] FOREIGN KEY ([CourseCode]) REFERENCES [Course]([Code]), 
    CONSTRAINT [FK_AcademicRecord_Student] FOREIGN KEY ([StudentId]) REFERENCES [Student]([Id]) 
)

CREATE TABLE [dbo].[Registration] (
    [Course_CourseID]    NVARCHAR (16) NOT NULL,
    [Student_StudentNum] NVARCHAR (16) NOT NULL,
    PRIMARY KEY CLUSTERED ([Course_CourseID] ASC, [Student_StudentNum] ASC),
    CONSTRAINT [FK_Registration_ToCourse] FOREIGN KEY ([Course_CourseID]) REFERENCES [dbo].[Course] ([Code]),
    CONSTRAINT [FK_Registration_ToStudent] FOREIGN KEY ([Student_StudentNum]) REFERENCES [dbo].[Student] ([Id])
);

CREATE TABLE [dbo].[Employee]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [UserName] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL
)

CREATE TABLE [dbo].[Role]
(
    [Id] INT,
    [Role] VARCHAR(100) NULL,
	CONSTRAINT [PK_Role] PRIMARY KEY ([Id])
);

CREATE TABLE [dbo].[Employee_Role]
(
    [Employee_Id] INT,
    [Role_Id] INT,
	CONSTRAINT [PK_Employee_Role] PRIMARY KEY ([Employee_Id], [Role_Id]),
	CONSTRAINT [FK_ToEmployee] FOREIGN KEY ([Employee_Id]) REFERENCES [dbo].[Employee] ([Id]),
    CONSTRAINT [FK_ToRole] FOREIGN KEY ([Role_Id]) REFERENCES [dbo].[Role] ([Id])
);


INSERT INTO [dbo].[Course] (Code, Title, HoursPerWeek, FeeBase, Description) VALUES ('CST0001', 'Web Programming', 4, 450, 'Students learn how to manage their laptop environment to gain the best advantage during their college program and later in the workplace. Create backups, install virus protection, manage files through a basic understanding of the Windows Operating System, install and configure the Windows Operating System, install and manage a virtual machine environment. Explore computer architecture including the functional hardware and software components that are needed to run programs. Finally, study basic numerical systems and operations including Boolean logic.');
INSERT INTO [dbo].[Course] (Code, Title, HoursPerWeek, FeeBase, Description) VALUES ('CST0002', 'Introduction to Computer Programming', 6, 650, 'Learn the fundamental problem-solving methodologies needed in software development, such as structured analysis, structured design, structured programming and introduction to object-oriented programming. Use pseudocode, flowcharting, as well as a programming language to develop solutions to real-world problems of increasing complexity. The basics of robust computer programming, with emphasis on correctness, structure, style and documentation are learned using Java. Theory is reinforced with application by means of practical laboratory assignments.');
INSERT INTO [dbo].[Course] (Code, Title, HoursPerWeek, FeeBase, Description) VALUES ('CST0003', 'Web Programming I', 4, 450, 'Students learn to develop websites with XHTML, CSS and JavaScript which emphasize structured and modular programming with an object-based paradigm. The course reinforces theory with practical lab assignments to create websites and to explore web-based applications that include client-side script');
INSERT INTO [dbo].[Course] (Code, Title, HoursPerWeek, FeeBase, Description) VALUES ('CST0004', 'Introduction to Database Systems', 8, 850, 'Students are introduced to the design and development of database systems using a current Database Management System (DBMS). Concepts and terminology of relational databases and design principles using the Entity Relationship model are presented. Students use SQL to create, modify and query a database. - See more at: http://www3.algonquincollege.com/sat/program/internet-applications-web-development/#courses');
INSERT INTO [dbo].[Course] (Code, Title, HoursPerWeek, FeeBase, Description) VALUES ('CST0005', 'Achieving Success in Changing Environments', 5, 550, 'Rapid changes in technology have created personal and employment choices that challenge each of us to find our place as contributing citizens in the emerging society. Life in the 21st century presents significant opportunities, but it also creates potential hazards and ethical problems that demand responsible solutions. Students explore the possibilities ahead, assess their own aptitudes and strengths, and apply critical thinking and decision-making tools to help resolve some of the important issues in our complex society with its competing interests. - See more at: http://www3.algonquincollege.com/sat/program/internet-applications-web-development/#courses');
INSERT INTO [dbo].[Course] (Code, Title, HoursPerWeek, FeeBase, Description) VALUES ('CST0006', 'Math Fundamentals', 4, 450, 'Students learn foundational mathematics required in many College technical programs. Students also solve measurement problems involving a variety of units and ratio and proportion problems. They manipulate algebraic expressions and solve equations. Students evaluate exponential and logarithmic expressions, study the trigonometry of right triangles and graph a variety of functions. - See more at: http://www3.algonquincollege.com/sat/program/internet-applications-web-development/#courses');
INSERT INTO [dbo].[Course] (Code, Title, HoursPerWeek, FeeBase, Description) VALUES ('CST0007', 'Database Systems', 6, 650, 'Students acquire practical experience using Oracle, an object-relational database management system. Advanced topics in database design are covered. Students have hands-on use of SQL, SQL scripts, PL/SQL and embedded SQL in host programs. Database concepts covered include data storage and retrieval, administration data warehouse, data mining, decision support, business intelligence, security and transaction control. Students also explore the use of open source database software. - See more at: http://www3.algonquincollege.com/sat/program/internet-applications-web-development/#courses');
INSERT INTO [dbo].[Course] (Code, Title, HoursPerWeek, FeeBase, Description) VALUES ('CST0008', 'Web Programming II', 3, 350, 'Through the study of C# and ASP.net, students learn the concepts of object-oriented programming as applied to the design, the development, and the debugging of ASP.net web. Object-oriented concepts, such as encapsulation, inheritance, abstraction and polymorphism are covered and reinforced with practical applications. The course also continues the development of Web Programming concepts by examining and using HTML form elements, HTML server controls and web server controls, the ASP.NET Page class, its inherent Page, Request, Response and Cookies objects. - See more at: http://www3.algonquincollege.com/sat/program/internet-applications-web-development/#courses');
INSERT INTO [dbo].[Course] (Code, Title, HoursPerWeek, FeeBase, Description) VALUES ('CST0009', 'Network Operating Systems', 6, 650, 'Students are introduced to the concepts behind implementing the Windows server and the Linux operating systems in a multiple user, computer and Internet Protocol (IP) networked environment. Topics include managing and updating user accounts, access rights to files and directories, Transmission Control Protocol/Internet Protocol (TCP/IP) and TCP/IP services: Domain Name System (DNS), Hyper Text Transfer Protocol (HTTP) and File Transfer Protocol (FTP). The course reinforces theory with practical lab assignments to install and configure both operating systems and the services mentioned. - See more at: http://www3.algonquincollege.com/sat/program/internet-applications-web-development/#courses');
INSERT INTO [dbo].[Course] (Code, Title, HoursPerWeek, FeeBase, Description) VALUES ('CST0010', 'Web Imaging and Animations', 4, 450, 'Students are introduced to basic concepts and techniques used to produce graphics, animations and video optimized for the World Wide Web. Students use Adobe software to create images and animations, build graphical user interfaces and author interactive applications. - See more at: http://www3.algonquincollege.com/sat/program/internet-applications-web-development/#courses');
INSERT INTO [dbo].[Course] (Code, Title, HoursPerWeek, FeeBase, Description) VALUES ('CST0011', 'Communications I', 8, 850, 'Communication remains an essential skill sought by employers, regardless of discipline or field of study. Using a practical, vocation-oriented approach, students focus on meeting the requirements of effective communication. Through a combination of lectures, exercises, and independent learning, students practise writing, speaking, reading, listening, locating and documenting information, and using technology to communicate professionally. Students develop and strengthen communication skills that contribute to success in both educational and workplace environments. - See more at: http://www3.algonquincollege.com/sat/program/internet-applications-web-development/#courses');
INSERT INTO [dbo].[Course] (Code, Title, HoursPerWeek, FeeBase, Description) VALUES ('CST0012', 'Web Programming Languages I', 6, 650, 'Emphasis is placed on ways of moving data between web pages and databases using the .NET platform: ASP, ADO, C#, and the .NET Framework. Heavy emphasis is placed on how web applications can interact with databases through ODBC or other technologies. Server-side methods and the advantages of multi-tiered applications are explored. The course concludes with a mini-project to develop a live web application that interacts with a database. - See more at: http://www3.algonquincollege.com/sat/program/internet-applications-web-development/#courses');


INSERT INTO [dbo].[Student] ([Id], [Name]) VALUES (N'S0001', N'Wei Gong')
INSERT INTO [dbo].[Student] ([Id], [Name]) VALUES (N'S0002', N'John Smith')
INSERT INTO [dbo].[Student] ([Id], [Name]) VALUES (N'S0003', N'Adam Smith')
INSERT INTO [dbo].[Student] ([Id], [Name]) VALUES (N'S0004', N'Jane Doe')
INSERT INTO [dbo].[Student] ([Id], [Name]) VALUES (N'S0006', N'Mary Davison')
INSERT INTO [dbo].[Student] ([Id], [Name]) VALUES (N'S0008', N'Peter Robinson')
INSERT INTO [dbo].[Student] ([Id], [Name]) VALUES (N'S0011', N'Mary Robinson')
INSERT INTO [dbo].[Student] ([Id], [Name]) VALUES (N'S0012', N'Stephen Harp')

INSERT INTO [dbo].[AcademicRecord] ([CourseCode], [StudentId], [Grade]) VALUES (N'CST0001', N'S0001', 70)
INSERT INTO [dbo].[AcademicRecord] ([CourseCode], [StudentId], [Grade]) VALUES (N'CST0002', N'S0001', 90)
INSERT INTO [dbo].[AcademicRecord] ([CourseCode], [StudentId], [Grade]) VALUES (N'CST0003', N'S0002', 70)
INSERT INTO [dbo].[AcademicRecord] ([CourseCode], [StudentId], [Grade]) VALUES (N'CST0005', N'S0003', 80)
INSERT INTO [dbo].[AcademicRecord] ([CourseCode], [StudentId], [Grade]) VALUES (N'CST0005', N'S0004', 90)
INSERT INTO [dbo].[AcademicRecord] ([CourseCode], [StudentId], [Grade]) VALUES (N'CST0005', N'S0006', 89)
INSERT INTO [dbo].[AcademicRecord] ([CourseCode], [StudentId], [Grade]) VALUES (N'CST0005', N'S0008', 85)
INSERT INTO [dbo].[AcademicRecord] ([CourseCode], [StudentId], [Grade]) VALUES (N'CST0001', N'S0011', 88)
INSERT INTO [dbo].[AcademicRecord] ([CourseCode], [StudentId], [Grade]) VALUES (N'CST0003', N'S0012', 65)

INSERT INTO [dbo].[Employee] (name, UserName, password) VALUES ('Wei Gong', 'gongw', 'Password1');
INSERT INTO [dbo].[Employee] (name, UserName, password) VALUES ('John Smith', 'smithj', 'Password1');
INSERT INTO [dbo].[Employee] (name, UserName, password) VALUES ('Jane Doe', 'doej', 'Password1');
INSERT INTO [dbo].[Employee] (name, UserName, password) VALUES ('Mary Robinson', 'robinm', 'Password1');
INSERT INTO [dbo].[Employee] (name, UserName, password) VALUES ('David Jones', 'jonesd', 'Password1');

INSERT INTO [dbo].[Role] (Id, Role) VALUES (1, 'Department Chair');
INSERT INTO [dbo].[Role] (Id, Role) VALUES (2, 'Coordinator');
INSERT INTO [dbo].[Role] (Id, Role) VALUES (3, 'Instructor');

INSERT INTO [dbo].[Employee_Role] (Employee_Id, Role_Id) VALUES (4, 1);
INSERT INTO [dbo].[Employee_Role] (Employee_Id, Role_Id) VALUES (1, 3);
INSERT INTO [dbo].[Employee_Role] (Employee_Id, Role_Id) VALUES (2, 2);
INSERT INTO [dbo].[Employee_Role] (Employee_Id, Role_Id) VALUES (3, 3);
INSERT INTO [dbo].[Employee_Role] (Employee_Id, Role_Id) VALUES (5, 2);
INSERT INTO [dbo].[Employee_Role] (Employee_Id, Role_Id) VALUES (5, 3);