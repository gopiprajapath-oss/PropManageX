import { ChangeDetectionStrategy, Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { InvoiceService, InvoiceDto } from '../invoice.service';

@Component({
  selector: 'app-invoice-list',
  standalone: true,
  imports: [CommonModule, CurrencyPipe, DatePipe, FormsModule],
  templateUrl: './invoice-list-component.html',
  styleUrl: './invoice-list-component.css',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class InvoiceListComponent implements OnInit {
  private invoiceService = inject(InvoiceService);

  invoices = signal<InvoiceDto[]>([]);
  showCreateForm = signal(false);
  isLoading = signal(true);

  newInvoice: Omit<InvoiceDto, 'invoiceID'> = {
    contractID: 0,
    period: '',
    amount: 0,
    dueDate: new Date().toISOString().split('T')[0], 
    status: 'Pending',
  };

  ngOnInit() {
    this.loadInvoices();
  }

  loadInvoices() {
    this.isLoading.set(true);
    this.invoiceService.getInvoices().subscribe({
      next: (data) => {
        this.invoices.set(data);
        this.isLoading.set(false);
      },
      error: (err) => {
        console.error('Error loading invoices', err);
        this.isLoading.set(false);
      }
    });
  }

  changeStatus(invoice: InvoiceDto, newStatus: string) {
    this.invoiceService.updateStatus(invoice.invoiceID, newStatus).subscribe({
      next: (updated) => {
        this.invoices.update(list => 
          list.map(inv => inv.invoiceID === updated.invoiceID ? updated : inv)
        );
      },
      error: (err) => console.error('Failed to update status', err)
    });
  }

  removeInvoice(id: number) {
    if (confirm('Are you sure you want to delete this invoice?')) {
      this.invoiceService.deleteInvoice(id).subscribe({
        next: () => {
          console.log('✅ Invoice deleted successfully');
          this.invoices.update(list => list.filter(inv => inv.invoiceID !== id));
        },
        error: (err) => {
          if (err.status !== 200) {
            console.error('❌ Actual Delete Error:', err);
            alert('Failed to delete invoice');
          } else {
            this.invoices.update(list => list.filter(inv => inv.invoiceID !== id));
          }
        }
      });
    }
  }

  onCreateInvoice() {
    this.invoiceService.createInvoice(this.newInvoice).subscribe({
      next: (created) => {
        this.invoices.update(prev => [created, ...prev]);
        this.showCreateForm.set(false);
        this.resetForm();
      },
      error: (err) => console.error('Failed to create invoice', err)
    });
  }

  private resetForm() {
    this.newInvoice = { 
      contractID: 0, 
      period: '', 
      amount: 0, 
      dueDate: new Date().toISOString().split('T')[0], 
      status: 'Pending' 
    };
  }
}