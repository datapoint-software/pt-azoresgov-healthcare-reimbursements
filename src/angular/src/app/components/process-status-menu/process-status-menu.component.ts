import { CommonModule } from "@angular/common";
import { Component, Input } from "@angular/core";
import { ProcessStatus } from "../../enums/process-status.enum";
import { RouterModule } from "@angular/router";

@Component({
  imports: [
    CommonModule,
    RouterModule
  ],
  selector: 'app-process-status-menu',
  standalone: true,
  templateUrl: './process-status-menu.component.html'
})
export class ProcessStatusMenuComponent {

  @Input({ required: true })
  id: string = undefined!;

  @Input({ required: true })
  number: string = undefined!;

  @Input({ required: true })
  status: ProcessStatus = undefined!;

  readonly ProcessStatus = ProcessStatus;
}
