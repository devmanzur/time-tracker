<template>
  <div>
    <aside class='w-64' aria-label='Sidebar'>
      <div class='overflow-y-auto py-4 px-3 bg-gray-50 h-screen rounded'>
        <BrandNavHeader/>
        <div>
          <ul class='space-y-2'>
            <div
              v-for='item in menu'
              :key='item.name'
            >
              <SingleNavItem v-if='!item.nested' :item='item' @toggle='toggleNavItem'/>
              <NestedNavItem v-else :is-selected='isSelected(item.route)' :item='item' @toggle='toggleNavItem'/>
            </div>

          </ul>
        </div>
      </div>
    </aside>

  </div>
</template>

<script>
import {
  ChartSquareBarIcon,
  ChartPieIcon,
  UsersIcon,
  UserGroupIcon,
  BriefcaseIcon,
  CalendarIcon,
  DocumentTextIcon,
  CurrencyDollarIcon,
  LoginIcon,
  AdjustmentsIcon,
  ShoppingBagIcon
} from '@vue-hero-icons/solid';

export default {
  data() {
    return {
      currentRoute: '/',
      defaultRoute: ''
    };
  },
  computed: {
    menu() {
      if (!this.isLoggedIn) {
        return [
          {name: 'Sign in', icon: LoginIcon, route: '/auth/login'}
        ];
      }
      const pages = [
        {name: 'Dashboard', icon: ChartSquareBarIcon, route: '/'}
      ];
      if (this.hasPermission('ViewUsersPage')) {
        pages.push({name: 'Gyms', icon: BriefcaseIcon, route: '/gym'});
      }

      if (this.hasPermission('ViewPermissionsPage')) {
        pages.push({
          name: 'User management', nested: true, icon: UserGroupIcon, route: '/users', nestedRoutes: [
            {name: 'Users', route: '/users'},
            {name: 'Roles', route: '/users/roles'},
            {name: 'Permissions', route: '/users/permissions'}
          ]
        });
      }
      if (this.hasPermission('ViewProductsPage')) {
        pages.push({
          name: 'People', nested: true, icon: UsersIcon, route: '/people', nestedRoutes: [
            {name: 'Members', route: '/people/members'},
            {name: 'Staff', route: '/people/staff'}
          ]
        });
      }
      if (this.hasPermission('ViewProductsPage')) {
        pages.push({
          name: 'Schedule', nested: true, icon: CalendarIcon, route: '/schedule', nestedRoutes: [
            {name: 'Appointments', route: '/schedule/appointments'},
            {name: 'Appointment types', route: '/schedule/appointment-types'},
            {name: 'Calendar', route: '/schedule/calendar'},
            {name: 'Events', route: '/schedule/events'},
            {name: 'Check-ins', route: '/schedule/checkins'}
          ]
        });
      }
      if (this.hasPermission('ViewProductsPage')) {
        pages.push({
          name: 'Plans', nested: true, icon: DocumentTextIcon, route: '/plans', nestedRoutes: [
            {name: 'Plans', route: '/plans'},
            {name: 'Plan categories', route: '/plans/categories'},
            {name: 'Discounts', route: '/plans/discounts'}
          ]
        });
      }
      if (this.hasPermission('ViewProductsPage')) {
        pages.push({
          name: 'Products', nested: true, icon: ShoppingBagIcon, route: '/products', nestedRoutes: [
            {name: 'Products', route: '/products'},
            {name: 'Product categories', route: '/products/categories'},
            {name: 'Pre-orders', route: '/products/pre-orders'},
            {name: 'Discounts', route: '/products/discounts'}
          ]
        });
      }
      if (this.hasPermission('ViewProductsPage')) {
        pages.push({
          name: 'Payments', nested: true, icon: CurrencyDollarIcon, route: '/payments', nestedRoutes: [
            {name: 'Invoices', route: '/payments/invoices'},
            {name: 'Transactions', route: '/payments/transactions'},
            {name: 'Daily deposits', route: '/payments/daily-deposits'}
          ]
        });
      }
      if (this.hasPermission('ViewProductsPage')) {
        pages.push({
          name: 'Reports', nested: true, icon: ChartPieIcon, route: '/reports', nestedRoutes: [
            {name: 'Pro metrics', route: '/reports/pro-metrics'},
            {name: 'Insights', route: '/reports/insights'},
            {name: 'Metrics', route: '/reports/metrics'},
            {name: 'Check-in metrics', route: '/reports/checkin-metrics'},
            {name: 'Check-in metrics', route: '/reports/checkin-metrics'},
            {name: 'Annual Revenue', route: '/reports/annual-revenue'},
            {name: 'Sales Tax', route: '/reports/sales-tax'},
            {name: 'Product sales', route: '/reports/product-sales'},
            {name: 'Payroll', route: '/reports/payroll'},
            {name: 'Birthdays', route: '/reports/birthdays'},
            {name: 'Expiring memberships', route: '/reports/membership-expires'},
            {name: 'Risk report', route: '/reports/risk'}
          ]
        });
      }
      if (this.hasPermission('ViewProductsPage')) {
        pages.push({
          name: 'Settings', nested: true, icon: AdjustmentsIcon, route: '/settings', nestedRoutes: [
            {name: 'Settings', route: '/settings'},
            {name: 'Worksheet setup', route: '/settings/worksheet'},
            {name: 'Manage plan', route: '/payments/plan-management'},
            {name: 'Daily deposits', route: '/payments/daily-deposits'},
            {name: 'Bank connection', route: '/payments/bank-connect'}
          ]
        });
      }
      return pages;
    },
    isLoggedIn() {
      return this.$auth.loggedIn;
    }
  },
  methods: {
    toggleNavItem(route) {
      if (!this.isSelected(route)) {
        this.currentRoute = route;
        return;
      }
      this.currentRoute = this.defaultRoute;
    },
    hasPermission(permission) {
      // if (permission === '') return true;
      // const permissions = this.pagePermissions;
      // return permissions?.find(x => x.name === permission);
      return true;
    },
    isSelected(route) {
      return this.currentRoute === route;
    }
  }
};
</script>

