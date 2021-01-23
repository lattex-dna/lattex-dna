﻿create or replace package body EMPLOYEE_PKG
as
  /*procedure New_Employee
   (
     --Ma_NV in EMPLOYEE_TABLE.ID_NUMBER%type,
     Ho in EMPLOYEE_TABLE.LAST_NAME%type,
     Ten in EMPLOYEE_TABLE.FIRST_NAME%type,
     Tuoi in EMPLOYEE_TABLE.AGE%type,
     Dien_Thoai in EMPLOYEE_TABLE.PHONE%type,
     p_Phong_Ban in EMPLOYEE_TABLE.DEPARTMENT%type,
     p_Vi_Tri in EMPLOYEE_TABLE.POSITION%type
   )--return EMPLOYEE_TABLE.ID_NUMBER%type
  is
  begin
    insert into EMPLOYEE_TABLE(LAST_NAME, FIRST_NAME, AGE, PHONE, DEPARTMENT, POSITION) values(Ho, Ten, Tuoi, Dien_Thoai, p_Phong_Ban, p_Vi_Tri);
    --return Ma_NV;
  end New_Employee;*/

  function Valid_Department(p_Depart in EMPLOYEE_TABLE.DEPARTMENT%type) return EMPLOYEE_TABLE.ID_NUMBER%type
    is
     p_department_id number;
     begin
       select count(*) into p_department_id from DEPARTMENT_TABLE where DEPARTMENT_TABLE.DEPARTMENT_ID = p_Depart;
       return 1;
       /*= v_department_id;*/
     exception
       when others then
       --return false;
       return -11;
  end Valid_Department;

  function Valid_Position(p_Pos in EMPLOYEE_TABLE.POSITION%type) return EMPLOYEE_TABLE.ID_NUMBER%type
    is
     p_position_id number;
     begin
       select count(*) into p_position_id from POSITION_TABLE where POSITION_TABLE.POSITION_ID = p_Pos;
       return 1;
       /*= v_position_id;*/
     exception
       when others then
       --return false;
       return -1;
  end Valid_Position;

  function Add_New_Employee(--p_Ma_NV in EMPLOYEE_TABLE.ID_NUMBER%type,
                            v_Ho in EMPLOYEE_TABLE.LAST_NAME%type,
                            v_Ten in EMPLOYEE_TABLE.FIRST_NAME%type,
                            v_Tuoi in EMPLOYEE_TABLE.AGE%type,
                            v_Dien_Thoai in EMPLOYEE_TABLE.PHONE%type,
                            p_Phong_Ban in EMPLOYEE_TABLE.DEPARTMENT%type,
                            p_Vi_Tri in EMPLOYEE_TABLE.POSITION%type
                            )
    return EMPLOYEE_TABLE.ID_NUMBER%type
    is
      p_ID_Number number;
      
      begin
        if(Valid_Department(p_Phong_Ban) = 1)
         then
               if(Employee_Pkg.Valid_Position(p_Vi_Tri) = 1)
                then
                   --alter sequence NUMBER_ASCENDING_SEQUENCE restart with 0;
                   
                   p_id_Number := NUMBER_ASCENDING_SEQUENCE.NEXTVAL;
                   --Employee_Pkg.New_Employee(v_Ho, v_Ten, v_Tuoi, v_Dien_Thoai, p_Phong_Ban, p_Vi_Tri);
                   insert into EMPLOYEE_TABLE(ID_NUMBER, LAST_NAME, FIRST_NAME, AGE, PHONE, DEPARTMENT, POSITION) values(p_ID_Number, v_Ho, v_Ten, v_Tuoi, v_Dien_Thoai, p_Phong_Ban, p_Vi_Tri);
                   commit;
                   dbms_output.put_line('Them nhan vien moi thanh cong');
                   return NUMBER_ASCENDING_SEQUENCE.CURRVAL;
                else
                  dbms_output.put_line('Ma vi tri nay khong ton tai!');
                  --p_id_Number := -1;
                  --return p_id_Number;
               end if;
         else
           dbms_output.put_line('Ma phong ban nay khong ton tai!');
           --p_id_Number := -11;
           --return p_id_Number;
         end if;
      end Add_New_Employee;

end EMPLOYEE_PKG;