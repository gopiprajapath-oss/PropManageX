import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { CollectionService } from '../collection.service';
import { InvoiceDto } from '../invoice.service';
import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';

@Component({
  selector: 'app-collection-list',
  standalone: true,
  imports: [CommonModule, CurrencyPipe, DatePipe],
  templateUrl: './collection-list.component.html',
  styleUrl: './collection-list.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CollectionListComponent implements OnInit {
  private collectionService = inject(CollectionService);

  collections = signal<InvoiceDto[]>([]);
  isLoading = signal(true);

  ngOnInit() {
    this.loadCollections();
  }

  loadCollections() {
  this.isLoading.set(true);

  this.collectionService.getPendingCollections().subscribe({
    next: (data) => {
      // 1. Log the raw data so you can see exactly what the backend sent!
      console.log('RAW API DATA:', data);

      if (!data || data.length === 0) {
        console.warn('API returned an empty array []');
        this.isLoading.set(false);
        return;
      }

      // 2. Bulletproof Filter: Ignores spaces and capitalization
      const filtered = data.filter(inv => {
        if (!inv.status) return false;
        
        // Clean the string: remove spaces and make it lowercase
        const cleanStatus = inv.status.trim().toLowerCase(); 
        
        return cleanStatus === 'overdue' || cleanStatus === 'pending';
      });
      
      console.log('FILTERED DATA:', filtered); // Check if this array has your items
      this.collections.set(filtered);
      this.isLoading.set(false);
    },
    error: (err) => {
      console.error('API Failed to load:', err);
      this.isLoading.set(false);
    }
  });
}

  markAsPaid(id: number) {
    if(confirm('Confirm payment receipt for this invoice?')) {
      this.collectionService.recordPayment(id).subscribe(() => {
        // Remove from the 'Pending Collections' list since it's now paid
        this.collections.update(list => list.filter(c => c.invoiceID !== id));
      });
    }
  }
}