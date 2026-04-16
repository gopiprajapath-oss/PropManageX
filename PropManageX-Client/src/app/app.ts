import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { LandingComponent } from './feature/landing/landing-component';
import { RegisterComponent} from './feature/auth/register-component/register-component';
import { LoginComponent } from './feature/auth/login-component/login-component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('PropManageX-Client');
}
