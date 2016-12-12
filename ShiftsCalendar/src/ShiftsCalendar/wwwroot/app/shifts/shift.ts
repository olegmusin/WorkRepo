import { Observable } from "rxjs/Observable";
import { Project} from "../projects/project";


export class Shift {
    id: number;
    date: Date;
    project: Project;
   // workers: IWorkerShift[];
}