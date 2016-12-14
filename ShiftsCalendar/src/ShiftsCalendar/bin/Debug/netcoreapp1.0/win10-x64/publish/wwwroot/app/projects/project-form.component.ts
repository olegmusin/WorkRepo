import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { Project } from "./project";
import { ProjectsService } from "./projects.service";


@Component({
    moduleId: module.id,
    selector: "project-form",
    templateUrl: "./project-form.component.html",
    styleUrls: ["/css/site.css"]
})
export class ProjectFormComponent implements OnInit {

    @Input()
    newProject: Project;
    errorMessage: string;
    @Output()
    prjAdded: EventEmitter<Project> = new EventEmitter<Project>();
    constructor(private _projectsService: ProjectsService) { }

    ngOnInit(): void {

        this.newProject = new Project();
       
    }

    onSubmit(projectCreated) {

        this._projectsService.addProject(projectCreated.value)
            .subscribe(response => this.newProject = response,
                        error => this.errorMessage = <any>error,
                        () => {
                            this.prjAdded.emit(this.newProject);
                            projectCreated.reset();
            });
    }
}