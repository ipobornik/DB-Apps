<?php
namespace ANSR\Propel;

class OrmConfig
{
    public static function connect(
        $driver = 'mysql',
        $dsn = 'mysql:host=localhost;dbname=bookstore',
        $user = 'root',
        $pass = '',
        $connectionName = 'bookstore'
    )
    {
        $serviceContainer = \Propel\Runtime\Propel::getServiceContainer();
        $serviceContainer->checkVersion('2.0.0-dev');
        $serviceContainer->setAdapterClass($connectionName, $driver);
        $manager = new \Propel\Runtime\Connection\ConnectionManagerSingle();
        $manager->setConfiguration(array (
            'classname' => 'Propel\\Runtime\\Connection\\ConnectionWrapper',
            'dsn' => $dsn,
            'user' => $user,
            'password' => $pass,
            'attributes' =>
                array (
                    'ATTR_EMULATE_PREPARES' => false,
                ),
        ));
        $manager->setName($connectionName);
        $serviceContainer->setConnectionManager($connectionName, $manager);
        $serviceContainer->setDefaultDatasource($connectionName);
    }
}