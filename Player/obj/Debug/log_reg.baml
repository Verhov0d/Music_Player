   M S B A M L   `   `   `  ����A  =Player, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:PlayerPlayer  5   	       Player.log_reg   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Player     5   	   b ^MaterialDesignThemes.Wpf, Version=4.0.0.2422, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  Вход��5	   	   $	��350��6   $	��300��6#   .��5
      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6    � ColumnDefinitions 5   
   �� 5      $��70 �6   �� 5      $	��230 �6   .+�5   
   �� $��Авторизация��6   $��30(�6'   $��0��65   $��2��6B   $��Center=�6V   $��Center=�6q   �� 5   
   $6��0Введите ваш логин и пароль��6   $��20(�66   $��1��6D   $��2��6Q   $��Center=�6e   $��Center=�6�   �� 5   
   $��Логин:��6   $��15(�6"   $��2��60   $��Center=�6=   $
��Left=�6X   �� 5   
   -   5         ��Name$ tb_log��5      $��2��6"   $��1��6/   
����� �$��Center=�6R   $��Center=�6m      MaterialDesignTextBox#��[  6�   �� 5   
   $��Пароль:��6   $��15(�6#   $��3��61   $��Center=�6>   $
��Left=�6Y   G� 5   
   -   5         G�Name$ pb_pass��5      $��3��6(   $��1��65   
����� �$��Center=�6W   $��Center=�6r     MaterialDesignPasswordBox#��[ 6�   �� 5   
   -   5         ��Name$ b_reg��5      $��Регистрация��6    $��5��6J   $��2��6W   �� 5   
   -   5        $ b_log��5      $��4��6    $��2��6-   $��Вход��6A   5      5       5!      