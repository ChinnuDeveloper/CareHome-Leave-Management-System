import { useEffect, useState } from "react";
import { getDepartments } from "../services/departmentService";
import { Container, Typography, List, ListItem } from "@mui/material"; 
import type { Department } from "../types/department";

export default function DepartmentsPage(){
    const [departments,setDepartments]=useState<Department[]>([]);

    useEffect(()=>{
        getDepartments().then(setDepartments);
    },[]);

    return(
   <Container>
    <Typography variant="h4">
        Departments
    </Typography>

    <List>
        {departments.map((d)=>(
            <ListItem key={d.departmentId}>{d.departmentName}</ListItem>
        ))}
    </List>
</Container>
    );
}

