create or replace trigger AUTO_ASCENDING_NUMBER_TRIGGER
  before insert
    on EMPLOYEE_TABLE
      referencing new as new
        for each row
          begin
               --Auto increasing by 1 in ID collumn
               select NUMBER_ASCENDING_SEQUENCE.nextval into :new.ID_NUMBER from dual; 
          end AUTO_ASCENDING_NUMBER_TRIGGER;
/
