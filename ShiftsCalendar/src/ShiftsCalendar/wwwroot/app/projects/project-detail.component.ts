import { Component, Input, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Project } from "./project";
import { ProjectsService } from "./projects.service";

@Component({
    moduleId: module.id,
  
    templateUrl: "project-detail.component.html",
    styleUrls: ["/css/site.css"]
})

export class ProjectDetailComponent implements OnInit {


    project: Project;
    pageTitle = "Project Details";
    constructor(private _projectsService: ProjectsService, private _route: ActivatedRoute) { }

    ngOnInit(): void {

      let id = this._route.snapshot.params["id"]; 

        this._projectsService.getProjectById(id)
            .subscribe(res => { this.project = res; });
    }
}