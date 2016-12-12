import { Shift } from "../shifts/shift";


export class Project {

    id: number;
    
    name: string;
    address: string;
    numberOfWorkers: number;
    shifts: Shift[];  
   
}