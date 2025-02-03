# MiniBankingProject


### Kpay

Mobile No 

Me - Another Person

### User Table D 
ID
Full Name
Mobile No
Balance
Pin => 123456


Bank => Deposit / Withdraw

### Deposit D

Deposit Api => Mobile No, Amount (+) => 1000 + (-1000)

### Withdraw D

Withdraw Api => Mobile No, Amount (+) => 1000 - (-1000)
Minimum Balance 10000

Validation
Check Balance if < 10000 => Cannot Withdraw

### Transfer D

From Mobile No =>
To Mobile No => 
Amount
Pin (check validation in BLL)
Notes

### Validation D
- Check From Mobile No exits ? 
- Check To Mobile No exits ?
- From Mobile No != To Mobile No
- Pin == Pin
- Check Balance
- From Mobile No Balace - 
- To Mobile No Balance +
- Add Transaction History
- Transaction (Complete)

Transaction Table
From Mobile No
To Mobile No
Amount
Dates
Notes

4 API

- Balance D
- Create Wallet User D
- Login D
- Change Pin D

- [- Phone No Change, - Forget Pin, - Reset Pin,- First-Time Login (PIN set up)]


dotnet ef dbcontext scaffold "Server=MSI\SQLEXPRESS2022; Database=MiniDigitalWallet; User Id=sa; Password=sasa; TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -Tbl_User -f
 
dotnet ef dbcontext scaffold "Server=MSI\SQLEXPRESS2022; Database=MiniDigitalWallet; User Id=sa; Password=sasa; TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -f

dotnet ef dbcontext scaffold "Server=MSI\\SQLEXPRESS2022; Database=MiniDigitalWallet; User Id=sa; Password=sasa; TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -f
