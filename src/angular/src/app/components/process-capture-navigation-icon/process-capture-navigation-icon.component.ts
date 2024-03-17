import { Component, Input } from "@angular/core";
import { LoadingSpinnerComponent } from "../loading-spinner/loading-spinner.component";

@Component({
  imports: [
    LoadingSpinnerComponent
  ],
  selector: 'app-process-capture-navigation-icon',
  standalone: true,
  styleUrl: './process-capture-navigation-icon.component.scss',
  templateUrl: './process-capture-navigation-icon.component.html'
})
export class ProcessCaptureNavigationIconComponent {

  @Input({ required: false })
  public enabled?: boolean;

  @Input({ required: true })
  public invalid: boolean = undefined!;

  @Input({ required: false })
  public locked: boolean = false;

  @Input({ required: true })
  public written: boolean = undefined!;

  @Input({ required: true })
  public writting: boolean = undefined!;
}
