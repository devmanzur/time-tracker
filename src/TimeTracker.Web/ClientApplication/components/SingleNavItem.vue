<template>
  <div>
    <nuxt-link
      :to='item.route'
      class='flex items-center p-2 text-base font-normal text-gray-500 rounded-lg dark:text-white hover:bg-gray-100 dark:hover:bg-gray-700'
      @click.native='toggleNavItem'
    >
      <component
        :is='item.icon'
        class='mr-6 flex-shrink-0 h-6 w-6'
        aria-hidden='true'
      />
      <span
        class='text-gray-900 flex-1'
      >{{ item.name }}</span>
    </nuxt-link>
  </div>
</template>
<script>
export default {
  name: 'SingleNavItem',
  props: {
    item: {
      type:Object,
      required:true
    },
  },
  methods:{
    toggleNavItem(){
      debugger;
      this.$emit('toggle', this.item.route);
    }
  }
};
</script>
