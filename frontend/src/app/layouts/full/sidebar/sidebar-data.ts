import { NavItem } from './nav-item/nav-item';

export const navItems: NavItem[] = [
  {
    navCap: 'Home',
  },
  {
    displayName: 'Dashboard',
    iconName: 'home',
    route: '/dashboard',
  },
  {
    navCap: 'Other',
  },
  {
    displayName: 'Categories',
    iconName: 'category',
    route: '/ui-components/categories',
  },
  {
    displayName: 'Reports',
    iconName: 'chart-pie',
    route: '/ui-components/chips',
  },
  {
    displayName: 'Notifications',
    iconName: 'bell',
    route: '/ui-components/forms',
  }
];
