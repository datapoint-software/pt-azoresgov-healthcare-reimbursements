import { Component } from "@angular/core";
import { HeaderComponent } from "../../components/header/header.component";
import { CommonModule } from "@angular/common";

@Component({
  imports: [
    CommonModule,
    HeaderComponent
  ],
  selector: 'app-process-creation',
  standalone: true,
  templateUrl: './process-creation.component.html'
})
export class ProcessCreationComponent {

}
