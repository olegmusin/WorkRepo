import { Component } from "@angular/core";

@Component({
  moduleId: module.id,
  selector: "sc-404",
  template: `
    <article class="template animated slideInRight"style="text-align: center">
      <h4>Nothing personal, but... 404 Response!</h4>
      <div>This page is not where you think it is.</div>
    </article>
  `
})
export class PageNotFoundComponent { }
