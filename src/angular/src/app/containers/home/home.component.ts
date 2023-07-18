import { Component } from "@angular/core";
import { HeaderComponent } from "../../components/header/header.component";

@Component({
  imports: [
    HeaderComponent
  ],
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html'
})
export class HomeComponent {

}
