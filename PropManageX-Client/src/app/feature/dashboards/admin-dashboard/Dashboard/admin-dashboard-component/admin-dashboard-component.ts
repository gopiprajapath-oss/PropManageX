import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { SidebarComponent } from "../../../../../shared/sidebar-component/sidebar-component";
import { HeaderComponent } from '../../../../../shared/header-component/header-component';

@Component({
  selector: 'app-admin-dashboard-component',
  standalone: true,
  imports: [
    RouterOutlet,
    CommonModule,
    SidebarComponent,
    HeaderComponent,
    SidebarComponent
],
  templateUrl: './admin-dashboard-component.html',
  styleUrl: './admin-dashboard-component.css',
})
export class AdminDashboardComponent {}
