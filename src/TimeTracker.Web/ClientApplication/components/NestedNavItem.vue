<template>
  <div>
    <button
      type='button'
      class='flex items-center p-2 w-full text-base font-normal text-gray-900 rounded-lg transition duration-75 group hover:bg-gray-100 dark:text-white dark:hover:bg-gray-700'
      aria-controls='dropdown-example' data-collapse-toggle='dropdown-example'
      @click='toggleNavItem'>
      <component
        :is='item.icon'
        :class="[
                  isSelected ? '' : '',
                  'mr-3 flex-shrink-0 h-6 w-6 text-gray-500' ,
                ]"
        aria-hidden='true'
      />
      <span class='flex-1 ml-3 text-left whitespace-nowrap'>{{ item.name }}</span>
      <component
        :is='toggle.upIcon'
        :class="[
                  isSelected ? '' : 'hidden',
                  '' ,
                ]"
        aria-hidden='true'
      />
      <component
        :is='toggle.downIcon'
        :class="[
                  isSelected ? 'hidden' : '',
                  '' ,
                ]"
        aria-hidden='true'
      />
    </button>
    <ul
      id='dropdown-example'
      :class="[
                isSelected
                  ? ''
                  : 'hidden',
                'py-2 space-y-2 bg-gray-100 rounded',
              ]">
      <nuxt-link
        v-for='nItem in item.nestedRoutes'
        :key='nItem.name'
        :to='nItem.route'
        :class="[
                  isSelected
                    ? ''
                    : '',
                  'flex items-center p-2 text-base font-normal text-gray-900 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-gray-700',
                ]">
      <span
        class='flex items-center pl-11 w-full text-base font-normal text-gray-900 rounded-lg transition duration-75 group hover:bg-gray-100 dark:text-white dark:hover:bg-gray-700'>{{
          nItem.name
        }}</span>
      </nuxt-link>
    </ul>
  </div>
</template>
<script>

import {
  ChevronDownIcon,
  ChevronUpIcon
} from '@vue-hero-icons/solid';

export default {
  name: 'NestedNavItem',
  data() {
    return {
      toggle: {
        downIcon: ChevronDownIcon,
        upIcon: ChevronUpIcon,
      }
    };
  },
  props: {
    isSelected: {
      type: Boolean,
      required: true
    },
    item: {
      type: Object,
      required: true
    }
  },
  methods: {
    toggleNavItem() {
      debugger;
      this.$emit('toggle', this.item.route);
    }
  }
};
</script>
