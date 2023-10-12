# 🎒 Backpack.SqlBuilder  🏗 

Welcome to Backpack.SqlBuilder. This is just a handy library that you can use to help fluently create the text for sql commands.


It supports the following common sql commands
 - Select
 - Insert
 - Update
 - Create Table

# Example
```cs
var selectcommand = SqlBuilder.Select()
    .From("TableA")
    .Join("TableB", "USING (Col1)")
    .Where("x >1")
    .GroupBy("Col1", "Col2")
    .Limit(1)
    .ToSql();
```