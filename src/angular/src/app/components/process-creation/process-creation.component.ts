import { Component } from "@angular/core";
import { RouterOutlet } from "@angular/router";
import { ProcessCreationNavigationComponent } from "../process-creation-navigation/process-creation-navigation.component";

@Component({
  imports: [ ProcessCreationNavigationComponent, RouterOutlet ],
  selector: 'app-process-creation',
  standalone: true,
  templateUrl: './process-creation.component.html'
})
export class ProcessCreationComponent {

}
