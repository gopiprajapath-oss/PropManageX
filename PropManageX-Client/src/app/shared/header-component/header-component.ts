import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TokenService } from '../../core/services/token.service';
import { Router } from '@angular/router';
 
@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './header-component.html',
  styleUrls: ['./header-component.css']
})
export class HeaderComponent {
  private tokenService = inject(TokenService);
  private router = inject(Router);

  
  user: { name : string ; email : string ; role : string } | any = null;
  ngOnInit() {
    this.user = this.tokenService.getUser();
    console.log('Decoded User from Token:', this.user);
  }

  logout() {
    this.tokenService.removeToken();
    
    this.router.navigate(['']);
    
  }
  showProfile = false;
  toggleProfile(){
    this.showProfile = !this.showProfile;
  }
}





 