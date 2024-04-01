import { Routes } from '@angular/router';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignInGuard } from './guards/sign-in/sign-in.guard';
import { LayoutComponent } from './components/layout/layout.component';
import { HomeComponent } from './components/home/home.component';

export const routes: Routes = [
  {
    canActivate: [ SignInGuard.canActivate ],
    component: SignInComponent,
    path: 'sign-in',
    providers: SignInGuard.providers
  },
  {
    component: LayoutComponent,
    path: '',
    pathMatch: 'prefix',
    children: [
      {
        component: HomeComponent,
        path: '',
        pathMatch: 'full'
      }
    ]
  }
];
