import { Component, OnInit, Input, Output } from "@angular/core";
import { ProjectsService } from "./projects.service";
import { Project } from "./project";

import "rxjs/Rx";

@Component({

        templateUrl: "app/projects/projects-list.component.html",
        styleUrls: ["/css/site.css"]

    })
export class ProjectsListComponent implements OnInit
    {
        isBusy: boolean;
        projects: Project[];
        errorMessage: string;

        constructor(private _projectsService: ProjectsService) {}

        ngOnInit(): void
        {
            this.isBusy = true;
            this._projectsService.getProjects()
                .subscribe(projects => this.projects = projects,
                    error => this.errorMessage = error,
                    () => this.isBusy = false);
        }

        addNewProject(addedProject: Project): void
        {
            console.log(JSON.stringify(addedProject));

            this.projects.push({
                    id: addedProject.id,
                    name: addedProject.name,
                    address: addedProject.address,
                    numberOfWorkers: addedProject.numberOfWorkers,
                    shifts: addedProject.shifts
                });
        }

        onDelete(project): void
        {
            console.log(project.id);
            this._projectsService.deleteProject(project).subscribe(response => response);
            this.projects.splice(this.projects.indexOf(project), 1);
        }
    }