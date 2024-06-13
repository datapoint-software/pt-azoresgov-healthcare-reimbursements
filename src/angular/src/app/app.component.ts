import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SuiLoadingOverlayComponent } from '@app/components/sui-loading-overlay/sui-loading-overlay.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ SuiLoadingOverlayComponent, RouterOutlet],
  templateUrl: 'app.component.html',
  styleUrl: 'app.component.scss'
})
export class AppComponent {
  title = 'azoresgov-healthcare-reimbursements';
}
