create or replace package EMPLOYEE_PKG
as
  /*procedure New_Employee
   (
     --Ma_NV in EMPLOYEE_TABLE.ID_NUMBER%type,
     Ho in EMPLOYEE_TABLE.LAST_NAME%type,
     Ten in EMPLOYEE_TABLE.FIRST_NAME%type,
     Tuoi in EMPLOYEE_TABLE.AGE%type,
     p_Phong_Ban in EMPLOYEE_TABLE.DEPARTMENT%type,
     p_Vi_Tri in EMPLOYEE_TABLE.POSITION%type,
     Dien_Thoai in EMPLOYEE_TABLE.PHONE%type
   );*/
  
  function Valid_Department(p_Depart in EMPLOYEE_TABLE.DEPARTMENT%type) return EMPLOYEE_TABLE.ID_NUMBER%type;
  function Valid_Position(p_Pos in EMPLOYEE_TABLE.POSITION%type) return EMPLOYEE_TABLE.ID_NUMBER%type;
  function Add_New_Employee(--p_Ma_NV in EMPLOYEE_TABLE.ID_NUMBER%type,
                            v_Ho in EMPLOYEE_TABLE.LAST_NAME%type,
                            v_Ten in EMPLOYEE_TABLE.FIRST_NAME%type,
                            v_Tuoi in EMPLOYEE_TABLE.AGE%type,
                            v_Dien_Thoai in EMPLOYEE_TABLE.PHONE%type,
                            p_Phong_Ban in EMPLOYEE_TABLE.DEPARTMENT%type,
                            p_Vi_Tri in EMPLOYEE_TABLE.POSITION%type
                            )
           return EMPLOYEE_TABLE.ID_NUMBER%type;

end;