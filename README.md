# **Weight Bridge System**  

![image](https://github.com/user-attachments/assets/6b9c394e-e302-4236-b3e5-3ca0577caea8)

## **Introduction**  
The **Weight Bridge System** is developed to manage gate in and gate out processes for trucks entering and exiting the terminal before and after import and export activities. It is designed to enhance operational efficiency, reduce errors, and provide an integrated platform for information exchange.

## **Features**  
- **Weight Scale Measurement**: Automatically read and update truck weight.  
- **Cargo Information Management**: Manage cargo details such as Bill of Lading, Shipping Note, and Commodity Description.  
- **Search and Filter**: Easily search for transactions based on date, Lorry No, and other filters.  
- **Monitoring and Reporting**: Real-time monitoring and detailed reporting of transactions.  

## **Project Structure**  
The project is divided into the following main modules:  
- **Weight Measurement Module**: For reading and updating truck weight data.  
- **Cargo Information Module**: For managing cargo details.  
- **Search and Report Module**: For searching, filtering, and generating reports.  
- **Monitoring Module**: Real-time monitoring of gate in and gate out activities.  

## **Technologies Used**  

### **Frontend**  
- **C1.Win.C1Ribbon**: For creating the user interface on Windows Forms.  
- **GrapeCity.Spreadsheet**: For displaying and processing spreadsheets in the UI.  

### **Backend**  
- **Spring Framework**: Including the following components:  
  - **Spring.Aop**: Supports aspect-oriented programming.  
  - **Spring.Core**: Provides core functionalities of Spring.  
  - **Spring.Data**: Manages database connections and operations.  
- **iBatis.NET**: Used for mapping data between the database and application objects.  
  - **IBatisNet.Common**  
  - **IBatisNet.DataMapper**  

### **Logging & Debugging**  
- **log4net**: For logging and error tracking in the application.  

### **Report & Export**  
- **Microsoft.ReportViewer.WinForms**: For displaying reports in Windows Forms UI.  

### **.NET Framework**  
- **System.Windows.Forms**: For creating the user interface on Windows Forms.  
- **System.Xml & System.Xml.Linq**: For XML data processing.  
- **System.Data & System.Data.DataSetExtensions**: For connecting to and processing data from the database.  
- **System.Drawing**: For rendering and processing graphics in the UI.  
- **System.Web & System.Web.Extensions**: For web functionalities.  
- **System.Configuration**: For managing application configuration settings.  

### **Custom Libraries**  
- **Tsb.Catos.Cm.Core & Tsb.Catos.Cm.Win**  
- **Tsb.Fontos.Core & Tsb.Fontos.Win**  
- **Tsb.Global.Libraries**  
- **Tsb.Product.WB.Common & Tsb.Product.WB.Config**  
  - These are custom libraries developed specifically for this project to support business-specific functionalities.  

## **Installation & Setup**  
1. Clone this repository.  
2. Open the solution in Visual Studio.  
3. Restore NuGet packages.  
4. Build the solution.  
5. Configure database connection strings in `app.config`.  
6. Run the application.  

## **Contact**  
For any questions or support, please contact the development team.  

---

Feel free to modify or expand upon this documentation as needed. If you require further changes, let me know!
