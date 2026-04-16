import { Component } from '@angular/core';
import {  HeaderComponent } from "../../../../shared/header-component/header-component";
import { SidebarComponent } from "../../../../shared/sidebar-component/sidebar-component";
import { RouterOutlet } from "@angular/router";

@Component({
  selector: 'app-manager-dashboard-component',
  imports: [SidebarComponent, RouterOutlet, HeaderComponent],
  templateUrl: './manager-dashboard-component.html',
  styleUrl: './manager-dashboard-component.css',
})
export class ManagerDashboardComponent {}
