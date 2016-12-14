import { Component, OnInit, Input } from "@angular/core";
import { ProjectsService } from "../projects/projects.service";
import { WorkersService } from "../workers/workers.service";
import { Project } from "../projects/project";
import { Worker } from "../workers/worker";
import { Shift } from "./shift";
import "rxjs/Rx";


@Component({
    moduleId: module.id,
    selector: "shifts-datelist",
    templateUrl: "./shifts-datelist.component.html",
    styleUrls: ["/css/site.css"],
    providers: [ProjectsService]
})
export class ShiftsDatelistComponent implements OnInit {

    @Input()
    project: Project;
    shifts: Shift[];
    @Input()
    worker: Worker;
    isBusy: boolean;
    errorMessage: string;
    constructor(private _projectsService: ProjectsService, private _workersService: WorkersService) { }

    ngOnInit(): void {
        this.isBusy = true;
        if (this.project != null){
            this._projectsService.getShiftsForProject(this.project)
                .subscribe(res => this.shifts = res,
                    error => this.errorMessage = error,
                    () =>  this.isBusy = false );
        } else if(this.worker != null) {

            this._workersService.getShiftsForWorker(this.worker)
                .subscribe(res => this.shifts = res,
                error => this.errorMessage = error,
                () => this.isBusy = false);
        }

    }

}