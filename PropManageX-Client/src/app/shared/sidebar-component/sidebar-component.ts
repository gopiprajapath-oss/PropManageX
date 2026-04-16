import { Component, ChangeDetectionStrategy, signal, computed, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TokenService } from '../../core/services/token.service';

interface MenuItem {
  label: string;
  path: string;
  roles: string[];
}

@Component({
  selector: 'app-sidebar',
  imports: [CommonModule, RouterModule],
  templateUrl: './sidebar-component.html',
  styleUrls: ['./sidebar-component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush,
  host: {
    'class': 'ui-sidebar-host'
  }
})
export class SidebarComponent implements OnInit {
  private tokenService = inject(TokenService);

  // Signal holding the decoded user data
  user = signal<{ name: string; email: string; role: string } | null>(null);

  // The Master Configuration: Every route mapped to allowed roles
  private readonly masterMenu: MenuItem[] = [



      { label: 'My Dashboard', path: '/user-dashboard/overview', roles: ['Tenant', 'User', 'Buyer'] },
    // --- General User / Prospect Routes ---
    { label: 'Explore Properties', path: '/user-dashboard/properties', roles: ['Tenant', 'User', 'Buyer'] },
    
    // --- Active Tenant Routes ---
  
    { label: 'My Agreements', path: '/user-dashboard/agreements', roles: ['Tenant', 'User', 'Buyer'] },
    { label: 'Maintenance', path: '/user-dashboard/maintenance', roles: ['Tenant', 'User', 'Buyer'] },
   


    // --- Agent Routes ---
    { label: 'Properties', path: '/agent-dashboard/properties', roles: ['Agent'] },
    { label: 'Leads', path: '/agent-dashboard/leads', roles: ['Agent'] },
    { label: 'Deals', path: '/agent-dashboard/deals', roles: ['Agent'] },
    { label: 'Site-Visit', path: '/agent-dashboard/site-visit', roles: ['Agent'] },
    { label: 'Contracts', path: '/agent-dashboard/contracts', roles: ['Agent'] },
    
    // --- Manager Routes ---
    { label: 'Overview', path: '/manager-dashboard/overview', roles: ['Manager'] },
    { label: 'Maintenance', path: '/manager-dashboard/maintenance', roles: ['Manager'] },
    { label: 'Vendors', path: '/manager-dashboard/vendors', roles: ['Manager'] },
    { label: 'Contracts', path: '/manager-dashboard/contracts', roles: ['Manager'] },
    { label: 'Renewals', path: '/manager-dashboard/renewals', roles: ['Manager'] },

    // --- Admin Routes ---
    { label: 'Properties', path: '/admin-dashboard/properties', roles: ['Admin'] },
    {label : 'Users', path: '/admin-dashboard/users', roles: ['Admin'] },
    { label: 'Reports', path: '/admin-dashboard/reports', roles: ['Admin'] },

    //--- Finance Analyst Routes ---
    { label: 'Invoices', path: '/finance-dashboard/invoices', roles: ['Finance Analyst'] },
    {label : 'Rent-Collections', path: '/finance-dashboard/collections', roles: ['Finance Analyst']  },
    { label: 'Financial Reports', path: '/finance-dashboard/reports', roles: ['Finance Analyst'] }

  ];

  // Computed signal: Reactively filters the menu based on the user's role
  filteredMenu = computed(() => {
    const currentUser = this.user();
    if (!currentUser || !currentUser.role) return [];
    
    return this.masterMenu.filter(item => item.roles.includes(currentUser.role));
  });

  ngOnInit(): void {
    const decodedUser = this.tokenService.getUser();
    if (decodedUser) {
      this.user.set(decodedUser);
    }
  }
}