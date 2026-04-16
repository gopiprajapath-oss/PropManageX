import { ChangeDetectionStrategy, Component, signal } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-landing',
  imports: [RouterLink],
  templateUrl: './landing-component.html',
  styleUrl: './landing-component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
  host: {
    'class': 'landing-host-wrapper'
  }
})
export class LandingComponent {
  // Signal to handle mobile menu state
  isMobileMenuOpen = signal(false);

  toggleMobileMenu(): void {
    this.isMobileMenuOpen.update(state => !state);
  }
}